﻿namespace Day07.Src;

public class Hand
{
    public Dictionary<string, int> ReadFile(string filePath)
    {
        var lines = File.ReadAllLines("C:\\Users\\htotbagi\\Downloads\\advent-of-code\\advent-of-code-2023\\2023\\Day07\\Day07.Src\\Data\\exampleData0.txt");

        Dictionary<string, int> data = new();

        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            data[parts[0]] = int.Parse(parts[1]);
        }
        return data;
    }


    public Dictionary<char, int> CountOccurences(string card)
    {
        Dictionary<char, int> result = new();

        foreach (char c in card)
        {
            if (result.ContainsKey(c))
            {
                result[c] += 1;
            }
            else
            {
                result[c] = 1;
            }
        }

        return result;
    }

    public int GetCardType(string card)
    {
        Dictionary<char, int> counts = CountOccurences(card);

        if (counts.ContainsValue(5))
            return 7; // Five of a kind

        if (counts.ContainsValue(4) && counts.ContainsValue(1))
            return 6; // Four of a kind

        if (counts.ContainsValue(3) && counts.ContainsValue(2))
            return 5; // Full house

        if (counts.ContainsValue(3) && counts.ContainsValue(1))
            return 4; // Three of a kind

        if (counts.Count(kv => kv.Value == 2) == 2 && counts.ContainsValue(1))
            return 3; // Two pair

        if (counts.ContainsValue(2) && counts.ContainsValue(1))
            return 2; // One pair

        return 1;
    }

    public int CompareHands(string hand1, string hand2)
    {
        int type1 = GetCardType(hand1);
        int type2 = GetCardType(hand2);

        if (type1 != type2)
        {
            return type2.CompareTo(type1);
        }
        else
        {
            for (int i = 0; i < hand1.Length; i++)
            {
                if (hand1[i] != hand2[i])
                {
                    return hand2[i].CompareTo(hand1[i]);
                }
            }
            return 0;
        }
    }

    public int CalculateWinnings(string filePath)
    {
        var handsAndBids = ReadFile(filePath);

        var orderedHands = handsAndBids.Keys.OrderByDescending(hand => GetCardType(hand));

        int totalWinnings = 0;
        int rank = 1;

        foreach (var hand in orderedHands)
        {
            totalWinnings += handsAndBids[hand] * rank;
            rank++;
        }

        return totalWinnings;
    }
}