using AocHelper;
using AocHelper.DataStructures;
using AocHelper.Utilities;

namespace AOC_24.Challenges;

internal class Day15 : IAocChallenge
{
    private static readonly Dictionary<char, Vector2> _dirMap = new()
    {
        ['^'] = Vector2.Down,
        ['>'] = Vector2.Right,
        ['v'] = Vector2.Up,
        ['<'] = Vector2.Left,
    };
    
    public int Day => 15;

    private readonly string[] _mapStr;
    private readonly Vector2[] _moves;

    public Day15()
    {
        string[] input = new FetchData(Day, 2024).ReadInput().TrimEnd().Split("\n\n");
        _mapStr = input[0].Split('\n');
        _moves = input[1]
            .Where(c => c != '\n')
            .Select(c => _dirMap[c])
            .ToArray();
    }

    public string Challenge1()
    {
        Dictionary<Vector2, IObject> map = ParseStandardMapMap(_mapStr[0].Split('\n'));
        KeyValuePair<Vector2, IObject> robotPos = map.First(o => o.Value.GetType() == typeof(Robot));
        var robot = new Pair<Vector2, Robot>(robotPos.Key, (Robot)robotPos.Value);
        
        foreach (Vector2 move in _moves)
        {
            if (robot.Second.CanMove(robot.First, move, map))
            {
                robot.Second.Move(robot.First, move, map);
                robot = new Pair<Vector2, Robot>(robot.First + move, robot.Second);
            }
        }

        long total = map
            .Where(o => o.Value.GetType() == typeof(Box))
            .Select(b => b.Key.X + 100 * b.Key.Y)
            .Sum();
        
        return total.ToString();
    }

    public string Challenge2()
    {
        throw new NotImplementedException();
    }

    private static Dictionary<Vector2, IObject> ParseStandardMapMap(string[] map)
    {
        Dictionary<Vector2, IObject> items = new();
        
        foreach (GridPointer<char> gp in Enumeration.EnumerateArray(map))
        {
            var pos = new Vector2(gp.Pos.Y, gp.Pos.X);
            switch (gp.Value)
            {
                case '#':
                    items.Add(pos, new Wall());
                    break;
                case 'O':
                    items.Add(pos, new Box());
                    break;
                case '@':
                    items.Add(pos, new Robot());
                    break;
            }
        }

        return items;
    }

    #region Data Structures

    private interface IObject
    {
        /// <summary>
        /// Check if the object can move in the desired direction
        /// </summary>
        /// <param name="pos">The current pos of the object</param>
        /// <param name="dir">The required direction to move</param>
        /// <param name="map">The map state</param>
        bool CanMove(Vector2 pos, Vector2 dir, Dictionary<Vector2, IObject> map);
        
        /// <summary>
        /// Move the current object in the given direction
        /// </summary>
        /// <param name="pos">The current pos of the object</param>
        /// <param name="dir">The required direction to move</param>
        /// <param name="map">The map state</param>
        void Move(Vector2 pos, Vector2 dir, Dictionary<Vector2, IObject> map);
    }

    private class Box : IObject
    {
        public bool CanMove(Vector2 pos, Vector2 dir, Dictionary<Vector2, IObject> map)
        {
            if (map.TryGetValue(pos + dir, out IObject? obj))
            {
                return obj.CanMove(pos + dir, dir, map);
            }
            
            return true;
        }

        public void Move(Vector2 pos, Vector2 dir, Dictionary<Vector2, IObject> map)
        {
            if (map.TryGetValue(pos + dir, out IObject? obj))
            {
                obj.Move(pos + dir, dir, map);
            }

            map[pos + dir] = map[pos];
            map.Remove(pos);
        }
    }
    
    private class Robot : Box;
    
    private class Wall : IObject
    {
        public bool CanMove(Vector2 pos, Vector2 dir, Dictionary<Vector2, IObject> map)
        {
            return false;
        }

        public void Move(Vector2 pos, Vector2 dir, Dictionary<Vector2, IObject> map) { }
    }

    #endregion End Data Structures
}