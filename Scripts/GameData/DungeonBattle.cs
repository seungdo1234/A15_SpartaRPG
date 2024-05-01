﻿using System.Reflection.Emit;
using System.Linq;

namespace TextRPG
{
    public class DungeonBattle
    {
        public Player player;
        private Enemy enemy;
        private List<Enemy> enemies;  // 여러 몬스터를 저장할 리스트
        
        // 던전 몬스터 랜덤 추출
        private int currentEnemyIndex;
        private Random random = new Random();

        // 이벤트로 사망 처리
        public event Action PlayerDied;
        public event Action EnemyDied;

        public DungeonBattle()
        {
            player = new Player("Tester");
            enemies = new List<Enemy>(); ;  // 몬스터를 저장할 리스트 초기화
        }

        public void CheckforBattle()
        {

            while (true)
            {
                Console.WriteLine("정말 던전에 진입하시겠습니까? 끝을 보시거나, 죽기 전까지 탈출하실 수 없습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 들어간다");
                Console.WriteLine("0. 나간다");
                
                string choice = Console.ReadLine();

                switch(choice)
                {
                    case "1":
                        AppearEnemy();
                        if (enemies.Any())  // 몬스터가 있는지 확인 후 전투 시작
                        {
                            BattleStart();
                        }
                        else
                        {
                            Console.WriteLine("전투할 몬스터가 없습니다.");
                        }
                        break;

                    default:
                        return;
                }
            } 

        }

        public void AppearEnemy()
        {
            enemies.Clear();
            int currentDungeonLevel = player.Level; // 임시로 집어 넣음, 원래는 던전 난이도를 집어 넣어야함
            List<Enemy> originalEnemies = EnemyDataManager.instance.GetSpawnMonsters(currentDungeonLevel);  // 몬스터 데이터 매니저에서 몬스터 리스트 가져오기
            enemies = new List<Enemy>(originalEnemies.Select(e => new Enemy(e))); // 깊은 복사를 통해 리스트 복제

            foreach (var enemy in enemies)
            {
                Console.WriteLine($"{enemy.Name}가 나타났습니다!");
            }
        }

        public void BattleStart()
        {
            dungeonBattle(); // 전투 시작
        }

        public void dungeonBattle()
        {
            Console.Clear();
            currentEnemyIndex = 0;

            while (currentEnemyIndex < enemies.Count)
            {
                enemy = enemies[currentEnemyIndex]; // 현재 적 업데이트
                while (enemy.Health > 0 && player.Health > 0)
                {
                    BattleText(enemy);
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            PlayerTurn(enemy);
                            if (enemy.Health <= 0)
                            {
                                BettlePlayerWinEnd();
                                break; // 내부 while 루프 벗어남
                            }
                            EnemyTurn(enemy);
                            break;

                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }

                    if (player.Health <= 0)
                    {
                        BettlePlayerLoseEnd();
                    }
                }

                currentEnemyIndex++; // 다음 적으로 이동
                if (currentEnemyIndex >= enemies.Count)
                {
                    Console.WriteLine("모든 적이 패배했습니다. 마을로 돌아갑니다.");
                }
            }
        }

        public void PlayerTurn(Enemy enemy)
        {
            Console.WriteLine($"{player.Name}의 공격!");

            if (player.Health > 0)
            {
                string attackResult = player.Attack(enemy); // 공격 결과 메시지 반환
                Console.WriteLine(attackResult); // 공격 결과를 출력

                if (enemy.Health <= 0)
                {
                    BettlePlayerWinEnd(); // 적 체력이 0 이하면 승리 처리
                }
            }
            else
            {
                BettlePlayerLoseEnd(); // 플레이어 체력이 0 이하면 패배 처리
            }

            Console.Clear(); // 콘솔 화면 지우기
        }

        public void EnemyTurn(Enemy enemy)
        {
            Console.WriteLine($"{enemy.Name}의 공격!");

            if (enemy.Health > 0)
            {
                string attackResult = enemy.Attack(player); // 공격 결과 메시지 반환
                Console.WriteLine(attackResult); // 공격 결과를 출력

                if (player.Health <= 0)
                {
                    BettlePlayerLoseEnd(); // 플리에어 체력이 0 이하면 패배
                }
            }
            else
            {
                BettlePlayerWinEnd(); // 플레이어 체력이 0 이하면 패배 처리
            }

            Console.Clear(); // 콘솔 화면 지우기
        }

        private void BattleText(Enemy enemy)
        {
            Console.WriteLine("몬스터");
            Console.WriteLine($"몬스터: {enemy.Name}");
            Console.WriteLine($"HP: {enemy.Health}/{enemy.MaxHealth}");
            Console.WriteLine($"공격력: {enemy.Atk}");

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.ePlayerClass})");
            Console.WriteLine($"HP {player.Health}/{player.MaxHealth}");

            Console.WriteLine("1. 공격");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해 주세요");
            Console.WriteLine(">> ");

        }

        private void BettlePlayerWinEnd()
        {
            Console.WriteLine($"{player.Name}이(가) 승리 하였습니다. 마을로 돌아갑니다.");
            Thread.Sleep(500);
            EnemyDied?.Invoke();
        }

        private void BettlePlayerLoseEnd()
        {
            Console.WriteLine($"{player.Name}이(가) 패배 하였습니다. 마을로 돌아갑니다.");
            Thread.Sleep(500);
            PlayerDied?.Invoke();
        }
    }
}