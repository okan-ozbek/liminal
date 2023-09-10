using UnityEngine;
using System.Collections.Generic;

namespace Statics
{
    public class GeneralAnimatorManager
    {
        public static float Snap(float floatToSnap, IList<Vector2> conditions, IReadOnlyList<float> results, bool includeNegative)
        {
            for (int index = 0; index < conditions.Count; index++)
            {
                if (floatToSnap > conditions[index].x && floatToSnap < conditions[index].y)
                {
                    return results[index];
                }

                if (!includeNegative) continue;
                
                if (floatToSnap < conditions[index].x && floatToSnap > conditions[index].y)
                {
                    return results[index];
                }
            }

            return floatToSnap;
        }
    }
}