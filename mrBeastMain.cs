using MelonLoader;
using BTD_Mod_Helper;
using mrbeast;
using System.Collections.Generic;
using System.Linq;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.TowerSets;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;
using UnityEngine;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Simulation.SMath;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers.Filters;
namespace mrBeastMain
{
    /// <summary>
    /// The main class that adds the core tower to the game
    /// </summary>
    public class mrbeast : ModTower
    {
        

        public override TowerSet TowerSet => TowerSet.Primary;
        public override bool Use2DModel => true;
        public override string BaseTower => TowerType.DartMonkey;
        public override int Cost => 700;

        public override int TopPathUpgrades => 5;
        public override int MiddlePathUpgrades => 5;
        public override int BottomPathUpgrades => 5;
        public override string Description => "wow! mr beast! :0";

        public override ParagonMode ParagonMode => ParagonMode.Base000;
        public override string DisplayName => "mr beast";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            towerModel.range += 20;
            var attackModel = towerModel.GetAttackModel();
            attackModel.range += 20;

            var projectile = attackModel.weapons[0].projectile;
            projectile.ApplyDisplay<MrBeastProjDisplay>();
            projectile.pierce += 2;
            projectile.GetDamageModel().damage += 3;
        }

        public override bool IsValidCrosspath(int[] tiers) =>
            ModHelper.HasMod("UltimateCrosspathing") || base.IsValidCrosspath(tiers);
        public override string Get2DTexture(int[] tiers)
        {
            return "mrbeast-Icon";
        }
    }
    public class MrBeastProjDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "MrBeastProjectile");
        }
    }
    public class StartYoutube : ModUpgrade<mrbeast>
    {
        public override int Path => TOP;
        public override int Tier => 1;
        public override int Cost => 440;
        public override string Portrait => "youtubeIcon";
        public override string Icon => "youtubeIcon";
        public override string DisplayName => "start youtube";

        public override string Description => "mr beast starts youtube and makes minor cash per round.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("BananaFarm-005").GetBehavior<PerRoundCashBonusTowerModel>().Duplicate());
            towerModel.GetBehavior<PerRoundCashBonusTowerModel>().cashPerRound = 100;
        }
    }
    public class LiveStream : ModUpgrade<mrbeast>
    {
        public override int Path => TOP;
        public override int Tier => 2;
        public override int Cost => 800;

        public override string DisplayName => "live streamer";
        public override string Portrait => "LiveStreamIcon";
        public override string Icon => "LiveStreamIcon";
        public override string Description => "makes even MORE MONEY!!!";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<PerRoundCashBonusTowerModel>().cashPerRound += 300;
        }
    }
    public class SilverPlayButton : ModUpgrade<mrbeast>
    {
        public override int Path => TOP;
        public override int Tier => 3;
        public override int Cost => 7000;

        public override string DisplayName => "silver play button";
        public override string Portrait => "silverPlayButton";
        public override string Icon => "silverPlayButton";
        public override string Description => "the first few steps to greatness";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<PerRoundCashBonusTowerModel>().cashPerRound += 1100;
        }
    }
    public class MillionViewers : ModUpgrade<mrbeast>
    {
        public override int Path => TOP;
        public override int Tier => 4;
        public override int Cost => 27250;

        public override string DisplayName => "millions of viewers (and $$$)";
        public override string Portrait => "richGuyIcon";
        public override string Icon => "richGuyIcon";
        public override string Description => "be rich, to a certain extent at least.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<PerRoundCashBonusTowerModel>().cashPerRound += 3000;
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("BananaFarm-005").GetBehavior<BonusLivesPerRoundModel>().Duplicate());
            towerModel.GetBehavior<BonusLivesPerRoundModel>().amount = 5;
            var attackModel = towerModel.GetAttackModel();
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("Alchemist-004").GetWeapon(1).Duplicate());
            towerModel.AddBehavior(new MonkeyCityIncomeSupportModel("_MonkeyCityIncomeSupport", true, 2f, null, "MonkeyCityBuff", "BuffIconVillagexx4"));
            attackModel.weapons[1].rate = 0.3f;
        }
    }
    public class RichBeast : ModUpgrade<mrbeast>
    {
        public override int Path => TOP;
        public override int Tier => 5;
        public override int Cost => 100000;

        public override string DisplayName => "rich beast";
        public override string Portrait => "mrBeastCool";
        public override string Icon => "mrBeastCool";
        public override string Description => "casually purchases the moon";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.GetBehavior<PerRoundCashBonusTowerModel>().cashPerRound += 15000;
            towerModel.GetBehavior<BonusLivesPerRoundModel>().amount += 5;
            var attackModel = towerModel.GetAttackModel();
        }
    }
    public class BeastBurger : ModUpgrade<mrbeast>
    {
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 500;
        public override string Portrait => "beastburgerIcon";
        public override string Icon => "beastburgerIcon";
        public override string DisplayName => "beast burgers";

        public override string Description => "shoots burgers which do high damage to all bloon types.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var projectile = attackModel.weapons[0].projectile;
            projectile.ApplyDisplay<BeastBurgerDisplay>();
            projectile.GetDamageModel().immuneBloonProperties = Il2Cpp.BloonProperties.None;
            projectile.GetDamageModel().damage += 5;
        }
    }
    public class BeastBurgerDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "beastBurgerProj");
        }
    }
    public class BeastBlast : ModUpgrade<mrbeast>
    {
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 1000;
        public override string Portrait => "mrbeastBlast";
        public override string Icon => "mrbeastBlast";
        public override string DisplayName => "beast blaster";

        public override string Description => "will blow up surronding bloons if they get close, stunning them with the power of mrbeast.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("BombShooter-400").GetWeapon().Duplicate());
            attackModel.weapons[1].rate = 4f;
        }
    }
    public class KarlCards : ModUpgrade<mrbeast>
    {
        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 3000;
        public override string Portrait => "karlJacobsIcon";
        public override string Icon => "karlJacobsIcon";
        public override string DisplayName => "karl jacob cards";

        public override string Description => "throws 5 karl jacob cards which shred smaller bloon types.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            attackModel.weapons[0].emission = new ArcEmissionModel("ArcEmissionModel_", 5, 0, 10, null, false);
            foreach (var projectile in towerModel.GetWeapons().Select(weaponModel => weaponModel.projectile))
            {
                projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Ceramic", "Ceramic",
                    1, 5, false, false));
                projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Fortified", "Fortified",
                    1, 5, false, false));
                projectile.ApplyDisplay<KarlJacobs>();
            }
        }
    }
    public class KarlJacobs : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "karlJacobsProj");
        }
    }

    public class execute : ModUpgrade<mrbeast>
    {
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 3000;
        public override string Portrait => "hannahKiticon";
        public override string Icon => "hannahKiticon";
        public override string DisplayName => "execute";

        public override string Description => "execute ability takes down the biggest MOAB, karl jacob cards more MOAB damage";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("MonkeyBuccaneer-050").GetAbility().Duplicate());
            towerModel.GetAbility().name = "execute";
            towerModel.GetAbility().cooldown *= 0.5f;
            foreach (var projectile in towerModel.GetWeapons().Select(weaponModel => weaponModel.projectile))
            {
                attackModel.weapons[0].projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Moabs", "Moabs",
                   1, 12, false, false));
            }
            
        }
    }
    public class BeastOnTop : ModUpgrade<mrbeast>
    {
        public override int Path => MIDDLE;
        public override int Tier => 5;
        public override int Cost => 88000;
        public override string Portrait => "mrBeastOnTop";
        public override string Icon => "mrBeastOnTop";
        public override string DisplayName => "beast on top";

        public override string Description => "mr beast on top || mr beast on top";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("MonkeyBuccaneer-040").GetAbility().Duplicate());
            towerModel.GetAbility(1).name = "executev2";
            towerModel.GetAbility(1).cooldown *= 0.001f;
            towerModel.GetAbility(1).activateOnPreLeak = true;
            attackModel.weapons[0].emission = new ArcEmissionModel("ArcEmissionModel_", 10, 0, 10, null, false);
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("BombShooter-050").GetWeapon().Duplicate());
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("DartlingGunner-050").GetWeapon().Duplicate());
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("SniperMonkey-500").GetWeapon().Duplicate());
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("IceMonkey-500").GetWeapon().Duplicate());
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.Rate *= 0.3f;
            }
        }
    }
    public class CharityWork : ModUpgrade<mrbeast>
    {
        public override int Path => BOTTOM;
        public override int Tier => 1;
        public override int Cost => 800;
        public override string Portrait => "mrBeastCharity";
        public override string Icon => "mrBeastCharity";
        public override string DisplayName => "charity work";

        public override string Description => "does some charitable acts to help nearby monkeys, so he increases their MOAB damage by +3";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            towerModel.AddBehavior(new DamageModifierSupportModel("BeastMOAB",true, "BeastMOABHELP", null, false, new DamageModifierForTagModel("DamageModifierForTagModel_Moabs", "Moabs",
                   1, 3, false, false)));
        }
    }
    public class CommunityService : ModUpgrade<mrbeast>
    {
        public override int Path => BOTTOM;
        public override int Tier => 2;
        public override int Cost => 1500;
        public override string Portrait => "mrBeastPhilanthropy";
        public override string Icon => "mrBeastPhilanthropy";
        public override string DisplayName => "community contribution";

        public override string Description => "contributes with other monkeys to increase pierce of himself and other towers.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.projectile.pierce += 10;
            }
            towerModel.AddBehavior(new PierceSupportModel("BeastPierce",true,10f,"BeastPierce",null,false,"beastburgerIcon","beastburgerIcon"));
        }
    }
    public class Education : ModUpgrade<mrbeast>
    {
        public override int Path => BOTTOM;
        public override int Tier => 3;
        public override int Cost => 6500;
        public override string Portrait => "mrBeastEducation";
        public override string Icon => "mrBeastEducation";
        public override string DisplayName => "beast education";

        public override string Description => "educate others of the power of MRBEAST6000!!! or just increases attack rate of nearby towers :/";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 20;
            var attackModel = towerModel.GetAttackModel();
            attackModel.range += 20;
            towerModel.AddBehavior(new RateSupportModel("BeastRate",0.66f,true,"BeastingRate",false,1,null,"Icon","Icon"));
        }
    }
    public class MrBeastSchool : ModUpgrade<mrbeast>
    {
        public override int Path => BOTTOM;
        public override int Tier => 4;
        public override int Cost => 20000;
        public override string Portrait => "mrBeastEducationMoney";
        public override string Icon => "mrBeastEducationMoney";
        public override string DisplayName => "mr beast school";

        public override string Description => "all students [nearby towers] learn the beast power and increase in most stats.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 10;
            var attackModel = towerModel.GetAttackModel();
            attackModel.range += 10;
            towerModel.GetBehavior<PierceSupportModel>().pierce = 5f;
            towerModel.GetBehavior<PierceSupportModel>().maxStackSize = 10;
            towerModel.GetBehavior<RateSupportModel>().maxStackSize = 10;
            towerModel.GetBehavior<DamageModifierSupportModel>().maxStackSize = 10;
        }
    }
    public class BecomeDaBeast : ModUpgrade<mrbeast>
    {
        public override int Path => BOTTOM;
        public override int Tier => 5;
        public override int Cost => 60000;
        public override string Portrait => "mrBeastEducationLast";
        public override string Icon => "mrBeastEducationLast";
        public override string DisplayName => "learn the beast";

        public override string Description => "Learn how to be like MrBeast!";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 30;
            var attackModel = towerModel.GetAttackModel();
            attackModel.range += 30;
            towerModel.AddBehavior(new DiscountZoneModel("mrbeastDiscount",0.33f,1,"mrBeastDiscountStack","mrBeastDiscountgroup",false,5,"silverPlayButton","silverPlayButton"));
            towerModel.GetBehavior<PierceSupportModel>().pierce *= 5f;
            towerModel.GetBehavior<RateSupportModel>().multiplier *= 0.2f;
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("GlueGunner-005").GetWeapon().Duplicate());
        }
    }

    public class BeBeast : ModParagonUpgrade<mrbeast>
    {
        public override int Cost => 600000;
        public override string Description => "Mrbeast6000, ohhhh ohhohhhhh.";
        public override string DisplayName => "MrBeast6000";
        public override string Portrait => "mrBeastParagon";
        public override string Icon => "mrBeastParagon";
        public override void ApplyUpgrade(TowerModel towerModel)
        {
            towerModel.range += 120;
            var attackModel = towerModel.GetAttackModel();
            attackModel.range += 120;
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("BananaFarm-005").GetBehavior<PerRoundCashBonusTowerModel>().Duplicate());
            towerModel.GetBehavior<PerRoundCashBonusTowerModel>().cashPerRound = 696969;
            towerModel.AddBehavior(new RateSupportModel("BeastRatev2", 0.01f, true, "BeastingRatev2", false, 1, null, "Icon", "Icon"));
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("GlueGunner-005").GetWeapon().Duplicate());
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("BombShooter-050").GetWeapon().Duplicate());
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("DartlingGunner-050").GetWeapon().Duplicate());
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("Alchemist-004").GetWeapon().Duplicate());
            attackModel.AddWeapon(Game.instance.model.GetTowerFromId("SuperMonkey-005").GetWeapon().Duplicate());
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("BananaFarm-005").GetBehavior<BonusLivesPerRoundModel>().Duplicate());
            towerModel.GetBehavior<BonusLivesPerRoundModel>().amount = 100;
            towerModel.GetDescendants<FilterInvisibleModel>().ForEach(model => model.isActive = false);
            towerModel.AddBehavior(Game.instance.model.GetTowerFromId("MonkeyBuccaneer-050").GetAbility().Duplicate());
            towerModel.GetAbility().cooldown *= 0.001f;
            foreach (var projectile in towerModel.GetWeapons().Select(weaponModel => weaponModel.projectile))
            {
                projectile.ApplyDisplay<KarlJacobs>();
            }
            foreach (var weaponModel in towerModel.GetWeapons())
            {
                weaponModel.Rate *= 0.05f;
            }
        }
    }
}