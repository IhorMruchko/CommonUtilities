using CommonUtilities.Conversion;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Testing;

public class ConversionTesting
{
    [Test]
    
    [TestCase(typeof(string), "", "")]
    [TestCase(typeof(string), "12", "12")]
    [TestCase(typeof(string), "blob", "blob")]
    
    [TestCase(typeof(char), "12a", 'a')]
    [TestCase(typeof(char), "--blob", 'b')]
    [TestCase(typeof(char), "-c", 'c')]
    
    [TestCase(typeof(bool), "true", true)]
    [TestCase(typeof(bool), "yes", true)]
    [TestCase(typeof(bool), "1", true)]
    [TestCase(typeof(bool), "false", false)]
    [TestCase(typeof(bool), "no", false)]
    [TestCase(typeof(bool), "0", false)]
    
    [TestCase(typeof(int), "2147483647", int.MaxValue)]
    [TestCase(typeof(int), "10", 10)]
    [TestCase(typeof(int), "0", 0)]
    [TestCase(typeof(int), "-10", -10)]
    [TestCase(typeof(int), "-2147483648", int.MinValue)]
    
    [TestCase(typeof(sbyte), "127sb", 127)]
    [TestCase(typeof(sbyte), "10sb", 10)]
    [TestCase(typeof(sbyte), "0sb", 0)]
    [TestCase(typeof(sbyte), "-128sb", -128)]
    
    [TestCase(typeof(byte), "255b", 255)]
    [TestCase(typeof(byte), "100b", 100)]
    [TestCase(typeof(byte), "0b", 0)]
    
    [TestCase(typeof(double), "1.7976931348623157E+308d", double.MaxValue)]
    [TestCase(typeof(double), "1,7976931348623157E+308d", double.MaxValue)]
    [TestCase(typeof(double), "12.4d", 12.4)]
    [TestCase(typeof(double), "12,4d", 12.4)]
    [TestCase(typeof(double), "12d", 12)]
    [TestCase(typeof(double), ",0d", 0)]
    [TestCase(typeof(double), "0d", 0)]
    [TestCase(typeof(double), "0,0d", 0)]
    [TestCase(typeof(double), "-12d", -12)]
    [TestCase(typeof(double), "-12,4d", -12.4)]
    [TestCase(typeof(double), "-12.4d", -12.4)]
    [TestCase(typeof(double), "-1.7976931348623157E+308d", double.MinValue)]
    [TestCase(typeof(double), "-1,7976931348623157E+308d", double.MinValue)]
    
    [TestCase(typeof(decimal), "12.4m", 12.4)]
    [TestCase(typeof(decimal), "12,4m", 12.4)]
    [TestCase(typeof(decimal), "12m", 12)]
    [TestCase(typeof(decimal), ",0m", 0)]
    [TestCase(typeof(decimal), "0m", 0)]
    [TestCase(typeof(decimal), "0,0m", 0)]
    [TestCase(typeof(decimal), "-12m", -12)]
    [TestCase(typeof(decimal), "-12,4m", -12.4)]
    [TestCase(typeof(decimal), "-12.4m", -12.4)]
    
    [TestCase(typeof(float), "3.40282346638528859e+38f", float.MaxValue)]
    [TestCase(typeof(float), "3,40282346638528859e+38f", float.MaxValue)]
    [TestCase(typeof(float), "12.4f", 12.4f)]
    [TestCase(typeof(float), "12,4f", 12.4f)]
    [TestCase(typeof(float), "12f", 12f)]
    [TestCase(typeof(float), ",0f", 0f)]
    [TestCase(typeof(float), "0f", 0f)]
    [TestCase(typeof(float), "0,0f", 0f)]
    [TestCase(typeof(float), "-12f", -12f)]
    [TestCase(typeof(float), "-12,4f", -12.4f)]
    [TestCase(typeof(float), "-12.4f", -12.4f)]
    [TestCase(typeof(float), "-3.40282346638528859e+38f", float.MinValue)]
    [TestCase(typeof(float), "-3,40282346638528859e+38f", float.MinValue)]
    
    [TestCase(typeof(DayOfWeek), "Monday", DayOfWeek.Monday)]
    [TestCase(typeof(DayOfWeek), "Tuesday", DayOfWeek.Tuesday)]
    [TestCase(typeof(DayOfWeek), "Wednesday", DayOfWeek.Wednesday)]
    [TestCase(typeof(DayOfWeek), "Thursday", DayOfWeek.Thursday)]
    [TestCase(typeof(DayOfWeek), "Friday", DayOfWeek.Friday)]
    [TestCase(typeof(DayOfWeek), "Saturday", DayOfWeek.Saturday)]
    [TestCase(typeof(DayOfWeek), "Sunday", DayOfWeek.Sunday)]
    public void GenericConversion_Success(Type from, string value, object expectedResult)
    {
        Assert.Multiple(() =>
        {
            Assert.That(ConversionManager.TryConvert(from, value, out var result), Is.True);
            Assert.That(result, Is.EqualTo(expectedResult));
            Assert.That(result.GetType(), Is.EqualTo(from));
        });
    }

    [Test]
    [TestCase(typeof(char), "1,1")]
    [TestCase(typeof(char), "12")]
    [TestCase(typeof(char), " ")]
    [TestCase(typeof(char), "")]
    [TestCase(typeof(char), "#")]
    
    [TestCase(typeof(bool), "1,1")]
    [TestCase(typeof(bool), "12")]
    [TestCase(typeof(bool), " ")]
    [TestCase(typeof(bool), "")]
    [TestCase(typeof(bool), "#")]
    [TestCase(typeof(bool), "hello")]
    
    [TestCase(typeof(int), "10u")]
    [TestCase(typeof(int), "10d")]
    [TestCase(typeof(int), "10f")]
    [TestCase(typeof(int), "1,1")]
    [TestCase(typeof(int), "-1,1")]
    
    [TestCase(typeof(sbyte), "128sb")]
    [TestCase(typeof(sbyte), "10,1d")]
    [TestCase(typeof(sbyte), "10,1f")]
    [TestCase(typeof(sbyte), "10,1")]
    [TestCase(typeof(sbyte), "0")]
    [TestCase(typeof(sbyte), "-1")]
    [TestCase(typeof(sbyte), "-10,1")]
    [TestCase(typeof(sbyte), "-10,1f")]
    [TestCase(typeof(sbyte), "-10,1d")]
    [TestCase(typeof(sbyte), "-129sb")]
    
    [TestCase(typeof(byte), "256b")]
    [TestCase(typeof(byte), "10d")]
    [TestCase(typeof(byte), "10f")]
    [TestCase(typeof(byte), "10")]
    [TestCase(typeof(byte), "1,1")]
    [TestCase(typeof(byte), "-1,1")]
    [TestCase(typeof(byte), "-1b")]
    
    [TestCase(typeof(double), "12,4f")]
    [TestCase(typeof(double), "12.4f")]
    [TestCase(typeof(double), "12,4m")]
    [TestCase(typeof(double), "12.4m")]
    [TestCase(typeof(double), "12,4")]
    [TestCase(typeof(double), "12.4")]
    [TestCase(typeof(double), "12")]
    [TestCase(typeof(double), ",0f")]
    [TestCase(typeof(double), ",0m")]
    [TestCase(typeof(double), "0")]
    [TestCase(typeof(double), "0,0f")]
    [TestCase(typeof(double), "0,0m")]
    [TestCase(typeof(double), "-12")]
    [TestCase(typeof(double), "-12,4")]
    [TestCase(typeof(double), "-12.4")]
    [TestCase(typeof(double), "-12,4m")]
    [TestCase(typeof(double), "-12.4m")]
    [TestCase(typeof(double), "-12,4f")]
    [TestCase(typeof(double), "-12.4f")]
    
    [TestCase(typeof(decimal), "12,4f")]
    [TestCase(typeof(decimal), "12.4f")]
    [TestCase(typeof(decimal), "12,4")]
    [TestCase(typeof(decimal), "12.4")]
    [TestCase(typeof(decimal), "12,4d")]
    [TestCase(typeof(decimal), "12.4d")]
    [TestCase(typeof(decimal), "12d")]
    [TestCase(typeof(decimal), "12f")]
    [TestCase(typeof(decimal), ",0f")]
    [TestCase(typeof(decimal), ",0")]
    [TestCase(typeof(decimal), "0")]
    [TestCase(typeof(decimal), "0,0f")]
    [TestCase(typeof(decimal), "0,0")]
    [TestCase(typeof(decimal), "-12,4")]
    [TestCase(typeof(decimal), "-12.4")]
    [TestCase(typeof(decimal), "-12,4d")]
    [TestCase(typeof(decimal), "-12.4d")]
    [TestCase(typeof(decimal), "-12,4f")]
    [TestCase(typeof(decimal), "-12.4f")]
    
    [TestCase(typeof(float), "12,4")]
    [TestCase(typeof(float), "12.4")]
    [TestCase(typeof(float), "12,4d")]
    [TestCase(typeof(float), "12.4d")]
    [TestCase(typeof(float), ",0")]
    [TestCase(typeof(float), ",0d")]
    [TestCase(typeof(float), "0")]
    [TestCase(typeof(float), "0,0")]
    [TestCase(typeof(float), "0,0d")]
    [TestCase(typeof(float), "-12,4d")]
    [TestCase(typeof(float), "-12.4d")]
    [TestCase(typeof(float), "-12,4")]
    [TestCase(typeof(float), "-12.4")]
    
    [TestCase(typeof(DayOfWeek), "10,3")]
    [TestCase(typeof(DayOfWeek), "10")]
    [TestCase(typeof(DayOfWeek), "-10")]
    [TestCase(typeof(DayOfWeek), "monday")]
    public void GenericConversion_Failure(Type from, string value)
    {
        Assert.Multiple(() =>
        {
            Assert.That(ConversionManager.TryConvert(from, value, out var result), Is.False);
            Assert.That(result, Is.Null);
        });
    }
}