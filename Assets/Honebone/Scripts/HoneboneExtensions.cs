using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HoneboneExtensions
{
    public static string ColorStr(this string str, Color color)
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + str + "</color>";
    }

    public static string ColorStr(this int value, Color color)
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + value.ToString() + "</color>";
    }
    public static string ToSpr(this string spriteName)
    {
        return $"<sprite name={spriteName}>";
    }

    public static int ToInt(this float value) { return Mathf.RoundToInt(value); }
    public static int Limit(this int value, int max) { return Mathf.Min(value, max); }

    public static bool Dice(this float fPercent)
    {
        float dice = UnityEngine.Random.value * 100.0f;
        return dice <= fPercent;
    }

    public static bool Dice(this int chance)
    {
        int dice = Random.Range(0, 100);
        return dice < chance;
    }

    public static int ChoiceWithWeight(this float[] weight)
    {
        float sum = 0;
        foreach (float c in weight)
        {
            sum += c;
        }
        float dice = Random.Range(0, sum);
        //Debug.Log(dice.ToString());
        for (int i = 0; i < weight.Length; i++)
        {
            if (dice < weight[i])
            {
                //Debug.Log(i.ToString());
                return i;
            }
            dice -= weight[i];
        }
        if (dice == sum) { return weight.Length - 1; }
        else
        {
            Debug.Log("error");
            return -1;
        }
    }
    public static int ChoiceWithWeight(this List<float> weight)
    {
        float sum = 0;
        foreach (float c in weight)
        {
            sum += c;
        }
        float dice = Random.Range(0, sum);
        //Debug.Log(dice.ToString());
        for (int i = 0; i < weight.Count; i++)
        {
            if (dice < weight[i])
            {
                //Debug.Log(i.ToString());
                return i;
            }
            dice -= weight[i];
        }
        if (dice == sum) { return weight.Count - 1; }
        else
        {
            Debug.Log("error");
            return -1;
        }
    }
    public static int ChoiceWithWeight(this List<int> weight)
    {
        float sum = 0;
        foreach (float c in weight)
        {
            sum += c;
        }
        float dice = Random.Range(0, sum);
        //Debug.Log(dice.ToString());
        for (int i = 0; i < weight.Count; i++)
        {
            if (dice < weight[i])
            {
                //Debug.Log(i.ToString());
                return i;
            }
            dice -= weight[i];
        }
        if (dice == sum) { return weight.Count - 1; }
        else
        {
            Debug.Log("error");
            return -1;
        }
    }
    public static int RandIndex(this int length)
    {
        return Random.Range(0, length);
    }

    public static string GetValueWithSign(this float value)
    {
        if (value >= 0) { return "+" + value.ToString(); }
        else { return value.ToString(); }
    }
    public static string GetValueWithSign(this int value)
    {
        if (value > 0) { return "+" + value.ToString(); }
        else { return value.ToString(); }
    }

    public static float GetPercent(this int value, int max)
    {
        return value * 100f / max;
    }

    public static int Sum(this List<int> list)
    {
        int sum = 0;
        foreach (int f in list)
        {
            sum += f;
        }
        return sum;
    }
    public static float Sum(this List<float> list)
    {
        float sum = 0;
        foreach (float f in list)
        {
            sum += f;
        }
        return sum;
    }

    /// <summary>重複なしでList追加</summary>
    public static void AddRangeWithNoOverlap<T>(this List<T> list, List<T> add)
    {
        foreach (T t in add)
        {
            if (!list.Contains(t)) { list.Add(t); }
        }
    }
    public static void RemoveList<T>(this List<T> list, List<T> remove)
    {
        foreach (T t in remove)
        {
            if (list.Contains(t)) { list.Remove(t); }
        }
    }

    /// <summary>重複なしで指定された個数の配列をランダムに取得　要素数<=指定個数の時はリスト全体を返す</summary>
    public static List<T> Sample<T>(this List<T> list, int amount)
    {
        if (list.Count <= amount) { return new List<T>(list); }

        List<T> pool = new List<T>(list);
        List<T> sample = new List<T>();
        int index;
        for (int i = 0; i < amount; i++)
        {
            index = pool.Count.RandIndex();
            sample.Add(pool[index]);
            pool.RemoveAt(index);
        }

        return sample;
    }

    public static T Choice<T>(this List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static List<T> Shuffle<T>(this List<T> list)
    {
        List<T> shuffle = new List<T>(list);
        for (int i = shuffle.Count; i > 1; i--)
        {
            int k = Random.Range(0, i);
            T value = shuffle[k];
            shuffle[k] = shuffle[i - 1];
            shuffle[i - 1] = value;
        }

        return shuffle;
    }

    public static int VectorToInt(this Vector2Int pos)
    {
        return pos.y * 9 + pos.x;
    }
    public static Vector2Int IntToVector(this int pos)
    {
        return new Vector2Int(pos % 9, pos / 9);
    }

    /// <summary>マス座標が盤外にあるかをチェック</summary>
    public static bool OutOfBoard(this Vector2Int vector)
    {
        if (vector.x < 0) { return true; }
        if (vector.x > 8) { return true; }
        if (vector.y < 0) { return true; }
        if (vector.y > 8) { return true; }
        return false;
    }
    /// <summary>マス座標が盤外にあるかをチェック</summary>
    public static bool OutOfBoard(this int posInt)
    {
        return posInt.IntToVector().OutOfBoard();
    }
}
