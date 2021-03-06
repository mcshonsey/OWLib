﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using OWLib;
using OWLib.Types.Chunk;
using STULib.Types;
using STULib.Types.AnimationList.x020;
using STULib.Types.AnimationList.x021;
using STULib.Types.Generic;
using static DataTool.Helper.STUHelper;
using static DataTool.Helper.IO;

namespace DataTool.FindLogic {
    public class AnimationInfo : IEquatable<AnimationInfo> {
        public Common.STUGUID GUID;
        public Common.STUGUID Skeleton;

        public override int GetHashCode() {
            return (GUID != null ? GUID.GetHashCode() : 0);
        }

        public bool Equals(AnimationInfo other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(GUID, other.GUID);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AnimationInfo) obj);
        }
    }
    
    public class Animation {
        public static void AddGUID(HashSet<AnimationInfo> animations, Common.STUGUID newElement, Common.STUGUID skeleton, Dictionary<ulong, ulong> replacements) {
            if (newElement == null) return;
            
            if (replacements.ContainsKey(newElement)) newElement = new Common.STUGUID(replacements[newElement]);
            AnimationInfo newAnim = new AnimationInfo {GUID = newElement, Skeleton = skeleton};

            if (animations.All(x => !Equals(x.GUID, newAnim.GUID))) {
                animations.Add(newAnim);
            } else {
                AnimationInfo existing = animations.FirstOrDefault(x => Equals(x, newAnim));
                if (existing == null) return;
                if (existing.GUID == null) existing.GUID = newAnim.GUID;
                if (existing.GUID == null) existing.Skeleton = newAnim.Skeleton;
            }
        }
        
        // public static HashSet<ModelInfo> Find(HashSet<ModelInfo> existingModels, Common.STUInstance instance) {
        //     return null;
        // }

        public static HashSet<AnimationInfo> FindChunked(HashSet<AnimationInfo> existingAnimations, HashSet<ModelInfo> models, Common.STUGUID animationGUID,
            Dictionary<ulong, ulong> replacements = null) {
            if (existingAnimations == null) {
                existingAnimations = new HashSet<AnimationInfo>();
            }

            if (replacements == null) replacements = new Dictionary<ulong, ulong>();
            if (animationGUID == null) return existingAnimations;
            if (replacements.ContainsKey(animationGUID)) animationGUID = new Common.STUGUID(replacements[animationGUID]);

            using (Stream chunkStream = OpenFile(animationGUID)) {
                if (chunkStream == null) {
                    return existingAnimations;
                }
                Chunked chunked = new Chunked(chunkStream, true, ChunkManager.Instance);
                
                CECE[] ceces = chunked.GetAllOfTypeFlat<CECE>();
                foreach (CECE cece in ceces) {
                    existingAnimations = FindAnimations(existingAnimations, models, new Common.STUGUID(cece.Data.animation), replacements);
                    // cece.Data.animation] = parent;
                    // FindAnimationsSoft(cece.Data.animation, sound, animList, replace, parsed, map, handler, models, layers, cece.Data.animation);
                }
                
                DMCE[] dmces = chunked.GetAllOfTypeFlat<DMCE>();
                foreach (DMCE dmce in dmces) {
                    HashSet<AnimationInfo> newAnims = new HashSet<AnimationInfo>();
                    Model.FindModels(models, new Common.STUGUID(dmce.Data.modelKey), replacements);
                    Model.FindModels(models, new Common.STUGUID(dmce.Data.materialKey), replacements);
                    newAnims = FindAnimations(newAnims, models, new Common.STUGUID(dmce.Data.animationKey), replacements);
                    
                    Model.AddGUID(models, new Common.STUGUID(dmce.Data.modelKey), new Dictionary<ulong, List<TextureInfo>>(), newAnims, replacements);
                }
            }
            return existingAnimations;
        }

        public static HashSet<AnimationInfo> FindAnimations(HashSet<AnimationInfo> existingAnimations, HashSet<ModelInfo> models,
            STUAnimationListSecondaryContainer container, Dictionary<ulong, ulong> replacements = null) {
            
            if (container.GetType() == typeof(STUAnimationListSecondardAnimationContainerA)) {
                STUAnimationListSecondardAnimationContainerA animationContainerA =
                    container as STUAnimationListSecondardAnimationContainerA;
                existingAnimations = FindAnimations(existingAnimations, models, animationContainerA?.AnimationWrapper?.Animation, replacements);
            }
            if (container.GetType() == typeof(STUAnimationListSecondardAnimationContainerB)) {
                STUAnimationListSecondardAnimationContainerB animationContainerB =
                    container as STUAnimationListSecondardAnimationContainerB;
                existingAnimations = FindAnimations(existingAnimations, models, animationContainerB?.AnimationWrapper?.Animation, replacements);
            }
            if (container.GetType() == typeof(STU_BB7A7240)) {
                STU_BB7A7240 listBB7A = container as STU_BB7A7240;
                foreach (STU_74173BA8 listBB7Asub in listBB7A?.m_134EE5BB) {
                    if (listBB7Asub?.m_AF632ACD != null) {
                        existingAnimations = FindAnimations(existingAnimations, models, listBB7Asub.m_AF632ACD, replacements);
                    }
                    if (listBB7Asub?.m_3868F518 != null) {
                        if (listBB7Asub?.m_3868F518.GetType() == typeof(STU_BB7A7240)) {
                            
                        }
                    }
                    if (listBB7A.m_0DE1BA16 != null) {
                        foreach (STU_40274C18 listBB7A_4027 in listBB7A.m_0DE1BA16) {
                            existingAnimations = FindAnimations(existingAnimations, models, listBB7A_4027.m_AF632ACD);
                        }
                    }
                }
            }
            
            return existingAnimations;
        }

        public static HashSet<AnimationInfo> FindAnimations(HashSet<AnimationInfo> existingAnimations, HashSet<ModelInfo> models, Common.STUGUID animationGUID, Dictionary<ulong, ulong> replacements=null, Common.STUGUID skeleton=null) {
            if (existingAnimations == null) {
                existingAnimations = new HashSet<AnimationInfo>();
            }

            if (replacements == null) replacements = new Dictionary<ulong, ulong>();
            if (animationGUID == null) return existingAnimations;
            if (replacements.ContainsKey(animationGUID)) animationGUID = new Common.STUGUID(replacements[animationGUID]);

            switch (GUID.Type(animationGUID)) {
                case 0x06:
                    using (Stream anim = OpenFile(animationGUID)) {
                        if (anim == null) {
                            break;
                        }
                        using (BinaryReader reader = new BinaryReader(anim)) {
                            anim.Position = 0x18L;
                            ulong infokey = reader.ReadUInt64();
                            existingAnimations = FindChunked(existingAnimations, models, new Common.STUGUID(infokey), replacements);
                        }
                    }
                    AddGUID(existingAnimations, animationGUID, skeleton, replacements);
                    break;
                case 0x21:
                    STUAnimationListInfo listInfo = GetInstance<STUAnimationListInfo>(animationGUID);
                    foreach (STUAnimationListInfoSub listInfoSubInfo in listInfo.SubInfos) {
                        existingAnimations = FindAnimations(existingAnimations, models, listInfoSubInfo?.SecondaryList,
                            replacements);
                        if (listInfoSubInfo?.AnimationContainer != null && listInfoSubInfo.AnimationContainer.Animations != null) {
                            foreach (STUAnimationListAnimationWrapper listAnimationWrapper in listInfoSubInfo.AnimationContainer.Animations) {
                                existingAnimations = FindAnimations(existingAnimations, models, listAnimationWrapper?.Animation,
                                    replacements, listInfoSubInfo.Skeleton);  // todo: is main skeleton?
                            }
                        }
                        if (listInfoSubInfo.m_9AD6CC25 != null) {
                            if (listInfoSubInfo.m_9AD6CC25.GetType() == typeof(STU_7D00A73D)) {
                                STU_7D00A73D infosub7D00Converted = listInfoSubInfo.m_9AD6CC25 as STU_7D00A73D;
                                if (infosub7D00Converted?.m_083DC038 != null) {
                                    foreach (STU_65DD9C84 sub7D00Sub in infosub7D00Converted.m_083DC038) {
                                        existingAnimations = FindAnimations(existingAnimations, models, sub7D00Sub?.Animation,
                                            replacements, listInfoSubInfo.Skeleton);  // todo: is main skeleton?
                                    }
                                }
                                
                            }
                        }
                        if (listInfoSubInfo.m_560940DC != null) {
                            if (listInfoSubInfo.m_560940DC.m_6CB79D25 != null) {
                                foreach (STU_BE20B7F5 subBE20 in listInfoSubInfo.m_560940DC.m_6CB79D25) {
                                    existingAnimations = FindAnimations(existingAnimations, models, subBE20?.Animation,
                                        replacements, listInfoSubInfo.Skeleton);  // todo: is main skeleton?
                                }
                            }
                        }
                    }
                    // if (!Equals(listInfo.SecondaryList, animationGUID)) {
                    //     existingAnimations = FindAnimations(existingAnimations, listInfo.SecondaryList, replacements);
                    // }
                    foreach (Common.STUGUID listInfoReference in listInfo.References) {
                        // if (GUID.Type(listInfoReference) != 0x21) continue;
                        existingAnimations = FindAnimations(existingAnimations, models, listInfoReference, replacements);
                    }
                    STUAnimationListAnimationWrapper[] wrappers =
                        GetAllInstances<STUAnimationListAnimationWrapper>(animationGUID);
                    foreach (STUAnimationListAnimationWrapper animationWrapper in wrappers) {
                        existingAnimations =
                            FindAnimations(existingAnimations, models, animationWrapper?.Animation, replacements);
                    }
                    break;
                case 0x20:
                    STUAnimationListAnimationWrapper[] wrappers2 =
                        GetAllInstances<STUAnimationListAnimationWrapper>(animationGUID);
                    foreach (STUAnimationListAnimationWrapper animationWrapper in wrappers2) {
                        existingAnimations =
                            FindAnimations(existingAnimations, models, animationWrapper?.Animation, replacements);
                    }
                    // STUAnimationListSecondary listSecondary = GetInstance<STUAnimationListSecondary>(animationGUID);
                    // if (listSecondary == null) break;
                    // foreach (STUAnimationListSecondaryContainer listSecondaryContainer in listSecondary.Containers) {
                    //     existingAnimations = FindAnimations(existingAnimations, listSecondaryContainer, replacements);
                    // }
                    // STU_FEB7DB23 listWeird = GetInstance<STU_FEB7DB23>(animationGUID);  // idk what this is but it has animations so I don't care
                    // if (listWeird != null) {
                    //     existingAnimations = FindAnimations(existingAnimations, listWeird.AnimationWrapper?.Animation, replacements);
                    // }
                    break;
                default:
                    Debugger.Log(0, "DataTool.FindLogic.Animation", $"[DataTool.FindLogic.Animation] Unhandled type: {GUID.Type(animationGUID):X3}\n");
                    break;
            }
            return existingAnimations;
        }
    }
}