namespace textrpg
{
    public class warriorskills
    {
        //public warriorskills()
        //{
        //    addskill(new skilldata("강베기", 1, false, 10, "적 하나에게 무기로 강력한 일격을 날립니다."));
        //    addskill(new skilldata("가로베기", 3, true, 20, "무기를 크게 휘둘러 최대 3명의 적을 공격합니다."));
        //    addskill(new skilldata("위기모면", 1, false, 30, "적 하나에게 잃은 체력 비례 피해를 2회 가격합니다."));
        //}

        //public override list<(int damage, bool iscrit)>? getskilldamages(int id, unit unit, int enemycount)
        //{
        //    skilldata? skill = skilllist.find(x => x.id == id);
        //    if (skill == null) return null; // 스킬id에 해당하는 스킬이 없으면 null 반환            

        //    return null;
        //}

        //private bool strongstrike(int id, unit player)
        //{
        //    skilldata skill = skilllist.find(x => x.id == id);
        //    list<(int damage, bool iscrit)> result = new list<(int damage, bool iscrit)>();

        //    player.costmana(skill.manacost); // 마나소모
        //    (int damage, bool iscrit) hit = player.attack(); // 1회 타격정보
        //    hit.damage = hit.damage * 2; // 스킬 데미지 계산

        //    result.add(hit);

        //    return result;
        //}

        //private list<(int damage, bool iscrit)> horizontalstrike(int id, unit player, int enemycount)
        //{
        //    skilldata skill = skilllist.find(x => x.id == id);
        //    list<(int damage, bool iscrit)> result = new list<(int damage, bool iscrit)>();
        //    int count = skill.maxtargetcount > enemycount ? enemycount : skill.maxtargetcount;

        //    player.costmana(skill.manacost);
        //    for (int i = 0; i < count; i++)
        //    {
        //        (int damage, bool iscrit) hit = player.attack(); // 1회 타격정보
        //        hit.damage = convert.toint32(math.round(hit.damage * 1.5f));
        //        result.add(hit);
        //    }

        //    return result;
        //}

        //private list<(int damage, bool iscrit)> crisisevasion(int id, unit player)
        //{
        //    skilldata skill = skilllist.find(x => x.id == id);
        //    list<(int damage, bool iscrit)> result = new list<(int damage, bool iscrit)>();

        //    player.costmana(skill.manacost);
        //    for (int i = 0; i < 2; i++)
        //    {
        //        (int damage, bool iscrit) hit = player.attack();
        //        float skillrate = (player.maxhealth - player.health + 150) * 0.01f; // 스킬 계수 계산
        //        hit.damage = convert.toint32(math.round(hit.damage * skillrate));  // 스킬 데미지 계산
        //        result.add(hit);
        //    }

        //    return result;
        //}
    }
}
