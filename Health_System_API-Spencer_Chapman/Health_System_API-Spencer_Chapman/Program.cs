using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Health_System_API_Spencer_Chapman
{
    class Program
    {
        static int health;
        static int maxHealth;
        
        static int shield;
        static int maxShield;

        static int lives;
        static string name;
        static string status;

        static int level;
        static int experience;
        static int levelUp;

        static bool test;

        static void HealthStatus()
        {
            if (health >= 75)
            {
                status = "Healthy";
            }
            else if (health >= 50)
            {
                status = "Hurt";
            }
            else if (health >= 25)
            {
                status = "Very Hurt";
            }
            else if (health >= 1)
            {
                status = "Severely Hurt";
            }
            else if (health == 0)
            {
                status = "Dead";
            }
        }
        static void Heal(int heal) //tested
        {
            if (heal < 0)
            {
                heal = 0;
                health = health + heal;
                
            }
            else if (heal >= 0)
            {
                health = health + heal;
            }
            
            if (health >= maxHealth)
            {
                health = maxHealth;
            }
            HealthStatus();
        }
        static void ShieldRegen(int regen) //tested
        {
            if (regen < 0)
            { 
                regen = 0;
                shield = shield + regen;
                
            }
            else if (regen >= 0)
            {
                shield = shield + regen;
            }
            if (shield >= maxShield)
            {
                shield = maxShield;
            }
        }
        static void TakeDamage(int damage) //tested
        {
            if (damage >= 0)
            {
                if (shield >= damage)
                {
                    shield = shield - damage;

                }
                else if (shield < damage)
                {
                    health = health - (damage - shield);
                    shield = 0;
                }

                if (health < 0)
                {
                    health = 0;
                }
            }
            if (damage < 0)
            {
               
            }
            if (test == false)
            {
                HealthStatus();
                Death();
            }
        }
        static void Death() //tested
        {
            if (health == 0)
            {
                lives = lives - 1;
                ShowHUD();
                health = maxHealth;
                HealthStatus();
            }
            if (lives < 0)
            {
                Console.WriteLine("");
                Console.WriteLine("-------------");
                Console.WriteLine("Game Over");
                Console.WriteLine("-------------");
                Console.WriteLine("");  
            }
        }
        static void GainLife(int add) //tested
        {
            if (add >= 0)
            {
                lives = lives + add;
            }
            if (add < 0)
            {
                
            }
            if (lives >= 99)
            {
                lives = 99;
            }    
        }
        static void XpGain(int xp) //tested
        {
            if (xp >= 0)
            {
                experience = experience + xp;
                LevelUp();
            }
            if (xp < 0)
            {
                
            }
        }
        static void LevelUp() //tested
        {
            if (experience >= levelUp)
            {
                experience = experience - levelUp;
                level = level + 1;
                levelUp = levelUp + (levelUp / 10);
            }
        }
        static void ShowHUD()
        {
            Console.WriteLine("");
            Console.WriteLine("------------------------");
            Console.WriteLine(name + " level: " + level);
            Console.WriteLine("Lives: " + lives);
            Console.WriteLine("Experience: " + experience);
            Console.WriteLine("Status: " + status);
            Console.WriteLine("Health: " + health + "  " + "Shield: " + shield + "%");
            Console.WriteLine("------------------------");
            Console.WriteLine("");
        }

        static void Reset()
        {
            name = "Musu";
            health = 100;
            maxHealth = 100;
            shield = 100;
            maxShield = 100;
            lives = 3;
            level = 1;
            levelUp = 1000;
            HealthStatus();
        }

        static void UnitTest()
        {
            test = true;

            Console.WriteLine("Testing Heal() respects health <= 100");
            health = 100;
            Heal(10);
            Debug.Assert(health <= 100);

            Console.WriteLine();
            Console.WriteLine("Testing Heal() error checking (negative heal)");
            health = 100;
            Heal(-10);
            Debug.Assert(health <= 100);

            Console.WriteLine();
            Console.WriteLine("Testing ShieldRegen() respects shield <= 100");
            shield = 100;
            ShieldRegen(10);
            Debug.Assert(shield <= 100);

            Console.WriteLine();
            Console.WriteLine("Testing ShieldRegen() error checking (negative heal)");
            shield = 100;
            ShieldRegen(-10);
            Debug.Assert(shield <= 100);

            Console.WriteLine();
            Console.WriteLine("Testing Death() error checking (negative lives)");
            lives = 0;
            Death();
            Debug.Assert(lives >= 0);

            Console.WriteLine();
            Console.WriteLine("Testing GainLife() error checking (negative lives)");
            lives = 0;
            GainLife(-1);
            Debug.Assert(lives >= 0);

            Console.WriteLine();
            Console.WriteLine("Testing GainLife() respects lives <= 99");
            lives = 99;
            GainLife(1);
            Debug.Assert(lives <= 99);

            Console.WriteLine();
            Console.WriteLine("Testing XpGain() error checking (negative xp)");
            experience = 0;
            XpGain(-1);
            Debug.Assert(experience >= 0);

            Console.WriteLine();
            Console.WriteLine("Testing TakeDamage() where shield > damage");
            shield = 100;
            health = 100;
            Console.WriteLine("health: " + health + " shield: " + shield);
            TakeDamage(50);
            Console.WriteLine("health: " + health + " shield: " + shield);
            Debug.Assert(shield >= 0);

            Console.WriteLine();
            Console.WriteLine("Testing TakeDamage() where shield < damage");
            shield = 100;
            health = 100;
            Console.WriteLine("health: " + health + " shield: " + shield);
            TakeDamage(150);
            Console.WriteLine("health: " + health + " shield: " + shield);
            Debug.Assert(shield >= 0);
            Debug.Assert(health >= 0);

            Console.WriteLine();
            Console.WriteLine("Testing TakeDamage() where health < damage");
            shield = 0;
            health = 100;
            Console.WriteLine("health: " + health + " shield: " + shield);
            TakeDamage(150);
            Console.WriteLine("health: " + health + " shield: " + shield);
            Debug.Assert(shield >= 0);
            Debug.Assert(health >= 0);

            Console.WriteLine();
            Console.WriteLine("Testing TakeDamage() where shield + health < damage");
            shield = 20;
            health = 100;
            Console.WriteLine("health: " + health + " shield: " + shield);
            TakeDamage(150);
            Console.WriteLine("health: " + health + " shield: " + shield);
            Debug.Assert(shield >= 0);
            Debug.Assert(health >= 0);

            Console.WriteLine();
            Console.WriteLine("Testing TakeDamage() error checking (negative damage)");
            shield = 100;
            health = 100;
            Console.WriteLine("health: " + health + " shield: " + shield);
            TakeDamage(-50);
            Console.WriteLine("health: " + health + " shield: " + shield);
            Debug.Assert(shield <= 100);
            Debug.Assert(health <= 100);

            Console.WriteLine();
            Console.WriteLine("Testing LevelUp() if experience is high enough, level up ");
            experience = 1000;
            levelUp = 1000;
            level = 1;
            Console.WriteLine("level: " + level + " experience: " + experience);
            LevelUp();
            Console.WriteLine("level: " + level + " experience: " + experience);
            Debug.Assert(level >= 2);
            Debug.Assert(experience <=999);

            Console.WriteLine();
            Console.WriteLine("Testing LevelUp() required experience multiplied by 10% ");
            experience = 1000;
            levelUp = 1000;
            level = 1;
            Console.WriteLine("level: " + level + " experience: " + experience + " level up: " + levelUp);
            LevelUp();
            Console.WriteLine("level: " + level + " experience: " + experience + " level up: " + levelUp);
            Debug.Assert(level >= 2);
            Debug.Assert(experience <= 999);
            Debug.Assert(levelUp >= 1001);

            test = false;
            Console.WriteLine();
            Console.WriteLine("press any key for fake gameplay");
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            name = "Musu";
            health = 100;
            maxHealth = 100;
            shield = 100;
            maxShield = 100;
            lives = 3;
            level = 1;
            levelUp = 1000;
            HealthStatus();

            UnitTest();

            Console.ReadKey(true);

            Reset();

            ShowHUD();
            Console.WriteLine("takes 25 damage");
            TakeDamage(25);
            ShowHUD();
            Console.WriteLine("takes 100 damage, more than what the shield has left");
            Console.WriteLine("damage spills over into health");
            TakeDamage(100);
            ShowHUD();
            Console.WriteLine("takes 25 damage");
            TakeDamage(25);
            ShowHUD();
            Console.WriteLine("heals 24 health");
            Heal(24);
            ShowHUD();
            Console.WriteLine("heals 24 health");
            Heal(24);
            ShowHUD();
            Console.WriteLine("attempts a negative heal, it doesnt work");
            Heal(-24);
            ShowHUD();
            Console.WriteLine("attempts negative damage, it doesnt work");
            TakeDamage(-5);
            ShowHUD();
            Console.WriteLine("20 damage");
            TakeDamage(20);
            ShowHUD();
            Console.WriteLine("tries to heal more than the max health");
            Heal(24);
            ShowHUD();
            Console.WriteLine("regens 20 shield");
            ShieldRegen(20);
            ShowHUD();
            Console.WriteLine("tries to regen negatie shield, doesnt work");
            ShieldRegen(-20);
            ShowHUD();
            Console.WriteLine("regens 100 shield, range check stops it from going over max shield");
            ShieldRegen(100);
            ShowHUD();
            Console.WriteLine("200 damage done to player, they lose a life");
            TakeDamage(200);
            Console.WriteLine("player respawns");
            ShowHUD();
            GainLife(1);
            Console.WriteLine("player gains a life");
            ShowHUD();
            GainLife(99);
            Console.WriteLine("player gains 99 lives, maxes out at cap");
            ShowHUD();
            GainLife(-5);
            Console.WriteLine("player gains negative lives, it doesnt work");
            ShowHUD();
            XpGain(250);
            Console.WriteLine("player gains xp");
            ShowHUD();
            XpGain(1000);
            Console.WriteLine("player gains 1000 xp, flows over levelUp. extra experience is kept, level goes up, along with required xp");
            ShowHUD();

            Console.ReadKey(true);
        }
    }
}
