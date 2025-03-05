using CommonUtilities.Console.Entities;
using CommonUtilities.Console.Entities.ArgumentBagParsingStates;

namespace CommonUtilities.Testing.Console.ArgumentBagTesting;

public class OptionalParameterValueParsingStateTests
{
    [Test]
    [TestCase(0, "-case=10")]
    [TestCase(0, "-case=", "10")]
    [TestCase(0, "-case=10=ab", "10")]
    [TestCase(0, "-case=10", "--testing")]
    [TestCase(0, "-case=", "10", "--testing")]
    [TestCase(0, "-case=10=10", "10", "--testing")]
    [TestCase(0, "-case", "--testing")]
    [TestCase(1, "--testing", "-case=10")]
    [TestCase(1, "--testing", "-case=10=10=10")]
    [TestCase(1, "--testing", "-case=", "10")]
    public void InconsistentSingleDash_ErrorParsingState(int index, params string[] args)
    {
        var argumentBag = new ArgumentBag();
        var optionalParsingState = new OptionalParameterValueParsingState
        {
            Context = argumentBag
        };
        
        var result = optionalParsingState.Parse(args, index);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<ErrorParsingState>());
            Assert.That(
                ((ErrorParsingState)argumentBag.ParsingState).ErrorMessage, 
                Is.EqualTo("Single dash should be followed by one sign name")
            );
        });
    }
    
    [Test]
    [TestCase(0, "--c=10")]
    [TestCase(0, "--c=", "10")]
    [TestCase(0, "--c=10=ab", "10")]
    [TestCase(0, "--c=10", "--testing")]
    [TestCase(0, "--c=", "10", "--testing")]
    [TestCase(0, "--c=10=10", "10", "--testing")]
    [TestCase(0, "--c", "--testing")]
    [TestCase(1, "--testing", "--c=10")]
    [TestCase(1, "--testing", "--c=10=10=10")]
    [TestCase(1, "--testing", "--c=", "10")]
    public void InconsistentDoubleDash_ErrorParsingState(int index, params string[] args)
    {
        var argumentBag = new ArgumentBag();
        var optionalParsingState = new OptionalParameterValueParsingState
        {
            Context = argumentBag
        };
        
        var result = optionalParsingState.Parse(args, index);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<ErrorParsingState>());
            Assert.That(
                ((ErrorParsingState)argumentBag.ParsingState).ErrorMessage, 
                Is.EqualTo("Double dash should not be followed by one sign name")
            );
        });
    }
    
    [Test]
    [TestCase(0, "a", "-a=", "10")]
    [TestCase(2, "best", "some", "test", "--best=", "10", "arguments")]
    public void SplitValue_ParsedParameter(
        int index, 
        string parameterName, 
        params string[] args
    )
    {
        var argumentBag = new ArgumentBag();
        var parsingState = new OptionalParameterValueParsingState
        {
            Context = argumentBag,
        }; 
        
        var result = parsingState.Parse(args, index);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index + 2));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<InitialParsingState>());
            Assert.That(argumentBag[parameterName].hasValue, Is.True);
            Assert.That(argumentBag[parameterName].value, Is.EqualTo(args[index + 1]));
        });
    }
    
    [Test]
    [TestCase(0, "a", "blob=asd", "-a=blob=asd", "10")]
    [TestCase(2, "best", "10=10=10=" ,"some", "test", "--best=10=10=10=", "10", "arguments")]
    public void MultipleSplitsValuie_ParsedParameter(
        int index, 
        string parameterName,
        string expectedValue,
        params string[] args
    )
    {
        var argumentBag = new ArgumentBag();
        var parsingState = new OptionalParameterValueParsingState
        {
            Context = argumentBag,
        }; 
        
        
        var result = parsingState.Parse(args, index);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index + 1));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<InitialParsingState>());
            Assert.That(argumentBag[parameterName].hasValue, Is.True);
            Assert.That(argumentBag[parameterName].value, Is.EqualTo(expectedValue));
        });
    }
    
    [Test]
    [TestCase(0, "a", "blob", "-a=blob", "10")]
    [TestCase(2, "best", "10" ,"some", "test", "--best=10", "10", "arguments")]
    public void DoubleSplit_InlineValue_ParsedParameter(
        int index, 
        string parameterName,
        string expectedValue,
        params string[] args
    )
    {
        var argumentBag = new ArgumentBag();
        var parsingState = new OptionalParameterValueParsingState
        {
            Context = argumentBag,
        }; 
        
        
        var result = parsingState.Parse(args, index);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index + 1));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<InitialParsingState>());
            Assert.That(argumentBag[parameterName].hasValue, Is.True);
            Assert.That(argumentBag[parameterName].value, Is.EqualTo(expectedValue));
        });
    }
}