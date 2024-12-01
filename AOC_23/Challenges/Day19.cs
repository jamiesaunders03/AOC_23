using System.Collections.Generic;
using System.Text.RegularExpressions;

using AocHelper;

using Range = AocHelper.DataStructures.Range;

namespace AOC_23.Challenges
{
    internal class Day19 : IAocChallenge
    {
        private enum ComparisonEnum
        {
            GT,
            LT,
            NONE,
        }

        private readonly struct WorkflowOption
        {
            public WorkflowOption(char property, ComparisonEnum comparison, int limit, string state)
            {
                Property = property;
                Limit = limit;
                Comparison = comparison;
                State = state;
            }

            public char Property { get; }
            public int Limit { get; }
            public ComparisonEnum Comparison { get; }
            public string State { get; }
        }

        private readonly struct Metal<T>
        {
            public T X { get; }
            public T M { get; }
            public T A { get; }
            public T S { get; }

            public Metal(T x, T m, T a, T s)
            {
                X = x;
                M = m;
                A = a;
                S = s;
            }
        }

        public int Day => 19;

        private const string START_WORKFLOW = "in";

        private static readonly Regex _workflowRe = new(@"(\w+){(.*?)}");
        private static readonly Regex _workflowItemRe = new(@"(\w)([<>])(\d+):(\w+)");
        private static readonly Regex _metalRe = new(@"{x=(\d+),m=(\d+),a=(\d+),s=(\d+)}");

        private readonly Dictionary<string, WorkflowOption[]> _workflows;
        private readonly Metal<int>[] _metals;

        public Day19()
        {
            string[] input = new FetchData(Day).ReadInput().TrimEnd().Split('\n');
            _workflows = new Dictionary<string, WorkflowOption[]>();

            int i = 0;
            while (input[i] != "")
            {
                Match m = _workflowRe.Match(input[i]);
                string name = m.Groups[1].Value;
                string body = m.Groups[2].Value;

                WorkflowOption[] options = body.Split(',').Select(ParseWorkflowOption).ToArray();
                _workflows[name] = options;
                ++i;
            }
            ++i;

            List<Metal<int>> metals = input.Skip(i).Select(ParseMetal).ToList();
            _metals = metals.ToArray();
        }

        public string Challenge1()
        {
            List<Metal<int>> accepted = new();
            List<(string, Metal<int>)> toProcess = _metals.Select(metal => new ValueTuple<string, Metal<int>>(START_WORKFLOW, metal)).ToList();

            while (toProcess.Count > 0)
            {
                (string workflowName, Metal<int> metal) next = toProcess[0];
                toProcess.RemoveAt(0);

                WorkflowOption[] workflow = _workflows[next.workflowName];
                string newWorkflow = GetGroup(next.metal, workflow);

                if (newWorkflow == "A")
                    accepted.Add(next.metal);
                else if (newWorkflow != "R")
                    toProcess.Add(new ValueTuple<string, Metal<int>>(newWorkflow, next.metal));
            }

            long total = accepted.Sum(m => m.A + m.M + m.S + m.X);
            return total.ToString();
        }

        public string Challenge2()
        {
            List<(string, Metal<Range>)> ranges = new()
            {
                (START_WORKFLOW, new Metal<Range>(
                    new Range(1, 4000), 
                    new Range(1, 4000), 
                    new Range(1, 4000), 
                    new Range(1, 4000))),
            };
            List<Metal<Range>> accepted = new();

            while (ranges.Count != 0)
            {
                (string wf, Metal<Range> rng) = ranges[0];
                ranges.RemoveAt(0);
                List<(string, Metal<Range>)> computedRanges = GetGroupRanges(rng, _workflows[wf]);

                foreach ((string wf, Metal<Range> rng) computedRange in computedRanges)
                {
                    if (computedRange.wf == "A")
                        accepted.Add(computedRange.rng);
                    else if (computedRange.wf != "R")
                        ranges.Add(computedRange);
                }
            }

            long total = accepted.Sum(GetElements);
            return total.ToString();
        }

        private static string GetGroup(Metal<int> m, WorkflowOption[] workflow)
        {
            foreach (WorkflowOption wo in workflow)
            {
                switch (wo.Comparison)
                {
                    case ComparisonEnum.NONE:
                        return wo.State;
                    case ComparisonEnum.LT when GetMetalProp(m, wo.Property) < wo.Limit:
                        return wo.State;
                    case ComparisonEnum.GT when GetMetalProp(m, wo.Property) > wo.Limit:
                        return wo.State;
                }
            }

            throw new Exception();
        }

        private static T GetMetalProp<T>(Metal<T> m, char prop)
        {
            return prop switch
            {
                'a' => m.A,
                'm' => m.M,
                's' => m.S,
                'x' => m.X,
                _ => throw new Exception(),
            };
        }

        private static List<(string, Metal<Range>)> GetGroupRanges(Metal<Range> range, WorkflowOption[] workflow)
        {
            List<(string, Metal<Range>)> ranges = new();
            foreach (WorkflowOption opt in workflow)
            {
                if (opt.Comparison == ComparisonEnum.NONE)
                {
                    ranges.Add((opt.State, range));
                    break;
                }
                else if (opt.Comparison == ComparisonEnum.LT)
                {

                }
                else
                {

                }
            }

            return ranges;
        }

        private static long GetElements(Metal<Range> range)
        {
            return range.X.Length * range.M.Length * range.A.Length * range.S.Length;
        }

        private static WorkflowOption ParseWorkflowOption(string section)
        {
            Match m = _workflowItemRe.Match(section);
            WorkflowOption option;
            if (!m.Success)
            {
                option = new WorkflowOption('\0', ComparisonEnum.NONE, 0, section);
            }
            else
            {
                char prop = m.Groups[1].Value[0];
                ComparisonEnum comp = m.Groups[2].Value == "<" ? ComparisonEnum.LT : ComparisonEnum.GT;
                int lim = int.Parse(m.Groups[3].Value);
                string next = m.Groups[4].Value;

                option = new WorkflowOption(prop, comp, lim, next);
            }

            return option;
        }

        private static Metal<int> ParseMetal(string line)
        {
            Match m = _metalRe.Match(line);

            return new Metal<int>(
                int.Parse(m.Groups[1].Value),
                int.Parse(m.Groups[2].Value),
                int.Parse(m.Groups[3].Value),
                int.Parse(m.Groups[4].Value)
            );
        }
    }
}
