using System;

namespace Infrastructure.BaseTools.Test
{
    public class DateToolsTest
    {
        public static IEnumerable<object[]> Dates
        {
            get
            {
                yield return new object[] { new KeyValuePair<string, DateTime>("1365/04/19", new DateTime(1986, 07, 10)) };
                yield return new object[] { new KeyValuePair<string,DateTime>("1402/02/04", new DateTime(2023, 4, 24)) };
                yield return new object[] { new KeyValuePair<string,DateTime>("1402/02/04", new DateTime(2023, 4, 24)) };
            }
        }

        public static IEnumerable<object[]> TimeStamps
        {
            get
            {
                yield return new object[] { new KeyValuePair<long, DateTime>(1682369369007, new DateTime(2023,04,24,20,49,29,7)) };
                yield return new object[] { new KeyValuePair<long, DateTime>(521337600000, new DateTime(1986, 7, 10,0,0,0)) };
                yield return new object[] { new KeyValuePair<long, DateTime>(0, new DateTime(1970, 1, 1,0,0,0)) };
            }
        }

        public static IEnumerable<object[]> DateTimes
        {
            get
            {
                yield return new object[] { new KeyValuePair<string, DateTime>("1365/04/19 17:10:12", new DateTime(1986, 07, 10, 17, 10, 12)) };
                yield return new object[] { new KeyValuePair<string, DateTime>("1365/04/19 03:10:12", new DateTime(1986, 07, 10, 3, 10, 12)) };
                yield return new object[] { new KeyValuePair<string, DateTime>("1402/02/04 00:00:00", new DateTime(2023, 4, 24, 0, 0, 0)) };
            }
        }
        [Fact]
        public void Shamsi_to_miladi_throw_exception_if_empty_or_null_passed()
        {
            // Arrange
            DateTools dateTools = new();

            // Act


            // Assert
            Assert.Throws<ArgumentNullException>(() => dateTools.ShamsiToMiladi(string.Empty));
            Assert.Throws<ArgumentNullException>(() => dateTools.ShamsiToMiladi(null!));
        }

        [Theory]
        [InlineData("1398111")]
        [InlineData("980101")]
        [InlineData("140101011")]
        public void Shamsi_to_miladi_throw_exception_if_short_string_passed(string shamsiDate)
        {
            // Arrange
            DateTools dateTools = new();

            // Act


            // Assert
            Assert.Throws<ArgumentException>(() => dateTools.ShamsiToMiladi(shamsiDate));
        }

        [Theory]
        [InlineData("1398/11/1/")]
        [InlineData("1398//01/01")]
        [InlineData("1398 /01/01")]
        [InlineData("1398 01 01 ")]
        [InlineData("1398 .01 01")]
        public void Shamsi_to_miladi_throw_exception_if_much_seperator_passed(string shamsiDate)
        {
            // Arrange
            DateTools dateTools = new();

            // Act


            // Assert
            Assert.Throws<ArgumentException>(() => dateTools.ShamsiToMiladi(shamsiDate));
        }

        [Theory]
        [InlineData("1398/11/1/")]
        [InlineData("1398//01/01")]
        public void Shamsi_to_miladi_throw_exception_if_much_slash_passed(string shamsiDate)
        {
            // Arrange
            DateTools dateTools = new();

            // Act


            // Assert
            Assert.Throws<ArgumentException>(() => dateTools.ShamsiToMiladi(shamsiDate));
        }

        [Theory]
        [InlineData("1398/15/01")]
        [InlineData("1398/07/31")]
        [InlineData("1398/04/32")]
        [InlineData("1398/04/-1")]
        public void Shamsi_to_miladi_throw_exception_if_invalid_data_passed(string shamsiDate)
        {
            // Arrange
            DateTools dateTools = new();

            // Act


            // Assert
            Assert.Throws<ArgumentException>(() => dateTools.ShamsiToMiladi(shamsiDate));
        }

        [Theory]
        [MemberData(nameof(Dates))]
        public void Shamsi_to_miladi_calculate_date_correctly(KeyValuePair<string,DateTime> dates)
        {
            // Arrange
            DateTools dateTools = new();

            // Act
            DateTime expDate = dates.Value.Date;

            // Assert
            Assert.Equal(expDate,dateTools.ShamsiToMiladi(dates.Key));
        }

        [Theory]
        [MemberData(nameof(Dates))]
        public void Miladi_to_shamsi_calculate_date_correctly(KeyValuePair<string, DateTime> dates)
        {
            // Arrange
            DateTools dateTools = new();

            // Act
            string expDate = dates.Key;

            // Assert
            Assert.Equal(expDate, dateTools.MiladiToShamsi(dates.Value));
        }

        [Theory]
        [MemberData(nameof(DateTimes))]
        public void Miladi_to_shamsi_calculate_date_time_correctly(KeyValuePair<string, DateTime> dates)
        {
            // Arrange
            DateTools dateTools = new();

            // Act
            string expDate = dates.Key;

            // Assert
            Assert.Equal(expDate, dateTools.MiladiToShamsi(dates.Value,true));
        }

        [Theory]
        [MemberData(nameof(TimeStamps))]
        public void Miladi_to_shamsi_calculate_time_stamps_from_date(KeyValuePair<long, DateTime> dates)
        {
            // Arrange
            DateTools dateTools = new();

            // Act
            long expTicks = dates.Key;

            // Assert
            Assert.Equal(expTicks, dateTools.ConvertToTimestamp(dates.Value));
        }

        [Theory]
        [MemberData(nameof(TimeStamps))]
        public void Miladi_to_shamsi_calculate_date_from_timestamp(KeyValuePair<long, DateTime> dates)
        {
            // Arrange
            DateTools dateTools = new();

            // Act
            DateTime expDate = dates.Value;

            // Assert
            Assert.Equal(expDate, dateTools.ConvertFromTimeStampToDateTime(dates.Key));
            Assert.Equal(expDate.ToLocalTime(), dateTools.ConvertFromTimeStampToDateTime(dates.Key,true));
        }

    }
}