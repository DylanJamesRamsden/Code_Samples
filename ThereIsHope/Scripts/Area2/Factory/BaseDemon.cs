using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class BaseDemon : Demon
    {

        public int attackDMG
        {
            get { return base.AttackDMG; }
            set { base.AttackDMG = value; }
        }

        public float movementSpeed
        {
            get { return base.MovementSpeed; }
            set { base.MovementSpeed = value; }
        }

        public int hp
        {
            get { return base.HP; }
            set { base.HP = value; }
        }


    public BaseDemon(int bonusDMG, float bonusMS, int Health)
        {
            attackDMG = 2 + bonusDMG;
            movementSpeed = 1f + bonusMS;
            hp = Health;
        }

    }
