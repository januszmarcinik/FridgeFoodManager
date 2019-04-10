using System;
using FluentAssertions;
using FridgeFoodManager.Api;
using Xunit;

namespace FridgeFoodManager.Tests.Unit
{
    public class SystemTimeTests
    {
        [Fact]
        public void SystemTimeNow_IfIsNotOverriden_ShouldReturnDateTimeNow()
        {
            var dateTimeNow = DateTime.Now;
            var systemTimeNow = SystemTime.Now;

            systemTimeNow.Should().NotBe(dateTimeNow);
            systemTimeNow.Should().BeCloseTo(dateTimeNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void SystemTimeNow_IfIsOverriden_ShouldReturnOverridenDate()
        {
            var dateTimeNow = DateTime.Now;
            SystemTime.NowFunc = () => dateTimeNow;
            var systemTimeNow = SystemTime.Now;

            dateTimeNow.Should().Be(systemTimeNow);
        }
    }
}
