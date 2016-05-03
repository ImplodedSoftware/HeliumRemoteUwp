using System;
using System.Collections.Generic;
using System.Text;

namespace NeonShared.Helpers
{
    public static class NeonHelpers
    {
        public static int UpsizeRating(int rating)
        {
            switch (rating)
            {
                case 1:
                    return NeonConstants.HELIUM_RATING_0_5;
                case 2:
                    return NeonConstants.HELIUM_RATING_1;
                case 3:
                    return NeonConstants.HELIUM_RATING_1_5;
                case 4:
                    return NeonConstants.HELIUM_RATING_2;
                case 5:
                    return NeonConstants.HELIUM_RATING_2_5;
                case 6:
                    return NeonConstants.HELIUM_RATING_3;
                case 7:
                    return NeonConstants.HELIUM_RATING_3_5;
                case 8:
                    return NeonConstants.HELIUM_RATING_4;
                case 9:
                    return NeonConstants.HELIUM_RATING_4_5;
                case 10:
                    return NeonConstants.HELIUM_RATING_5;
            }
            return 0;
        }
        public static int DownsizeRating(int rating)
        {
            switch (rating)
            {
                case NeonConstants.HELIUM_RATING_5:
                    return 10;
                case NeonConstants.HELIUM_RATING_4_5:
                    return 9;
                case NeonConstants.HELIUM_RATING_4:
                    return 8;
                case NeonConstants.HELIUM_RATING_3_5:
                    return 7;
                case NeonConstants.HELIUM_RATING_3:
                    return 6;
                case NeonConstants.HELIUM_RATING_2_5:
                    return 5;
                case NeonConstants.HELIUM_RATING_2:
                    return 4;
                case NeonConstants.HELIUM_RATING_1_5:
                    return 3;
                case NeonConstants.HELIUM_RATING_1:
                    return 2;
                case NeonConstants.HELIUM_RATING_0_5:
                    return 1;
            }
            return 0;
        }

        public static string FormatTime(long duration, bool includeHour = false)
        {
            if (duration < 0)
                duration = 0;
            if (includeHour)
            {
                var h = duration / 3600;
                duration = duration - (h * 3600);
                var m = duration / 60;
                duration = duration - (m * 60);
                return String.Format("{0:00}:{1:00}:{2:00}", h, m, duration);
            }
            else
            {
                var m = duration / 60;
                duration = duration - (m * 60);
                return String.Format("{0:00}:{1:00}", m, duration);
            }
        }

    }
}
