using System;
using Xunit;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void Test1() 
        {
            Assert.True(true);
        }

        [Fact]
        public void Sometest(){
            Assert.True(45 < 46);
        }

        [Fact]
        public void SomeotherTest(){
            Assert.True(50 < 51);
        }
    }
}
