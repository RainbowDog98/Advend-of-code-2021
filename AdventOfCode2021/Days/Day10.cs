﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    class Day10 : AdventOfCode
    {
        private readonly string[] _input = File.ReadAllLines("../../../Inputs/Input10.txt");
        private readonly Dictionary<char, int> lookup = new();

        public Day10()
        {
            lookup.Add(')', 3);
            lookup.Add(']', 57);
            lookup.Add('}', 1197);
            lookup.Add('>', 25137);
            lookup.Add('(', 1);
            lookup.Add('[', 2);
            lookup.Add('{', 3);
            lookup.Add('<', 4);
        }
        public override void PartOne()
        {
            int sum = 0;
            foreach (var line in _input)
            {
                Stack<char> stack = new();

                foreach (var ch in line)
                {
                    if (!IsClosing(ch))
                        stack.Push(ch);
                    else
                    {
                        var c = stack.Pop(); // opening
                        if(!Match(c, ch))
                        {
                            sum += lookup[ch];
                            break;
                        }
                    }
                }
            }
            Console.WriteLine($"Part 1: {sum}");
        }
        public override void PartTwo()
        {
            List<long> sums = new();
            bool wasCorrupted;
            foreach (var line in _input)
            {
                Stack<char> stack = new();
                wasCorrupted = false;
                foreach (var ch in line)
                {
                    if (!IsClosing(ch))
                        stack.Push(ch);
                    else
                    {
                        var c = stack.Pop(); // opening
                        if (!Match(c, ch))
                        {
                            wasCorrupted = true;
                            break;
                        }
                    }
                }
                if(stack.Count != 0 && !wasCorrupted) // found incomplete
                {
                    long current = 0;
                    foreach (var item in stack)
                    {
                        current = current * 5 + lookup[item];
                    }

                    sums.Add(current);
                }
            }

            var ordered = sums.OrderBy(x => x).ToArray();

            Console.WriteLine($"Part 2: {ordered[ordered.Length / 2]}");
        }
        private bool Match(char op, char cl)
        {
            return op switch
            {
                '(' => cl == ')',
                '[' => cl == ']',
                '{' => cl == '}',
                '<' => cl == '>',
                _ => false,
            };
        }
        private bool IsClosing(char c)
        {
            return c switch
            {
                ')' => true,
                ']' => true,
                '}' => true,
                '>' => true,
                _ => false,
            };
        }
    }
}
