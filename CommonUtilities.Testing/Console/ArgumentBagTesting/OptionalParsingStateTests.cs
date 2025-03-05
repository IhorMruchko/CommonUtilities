using CommonUtilities.Console.Entities;
using CommonUtilities.Console.Entities.ArgumentBagParsingStates;

namespace CommonUtilities.Testing.Console.ArgumentBagTesting;

public class OptionalParsingStateTests
{
    [Test]
    [TestCase(0, "---testing")]
    [TestCase(1, "--testing", "---blob")]
    public void ToManySigns_ErrorParsingState(int index, params string[] args)
    {
        var argumentBag = new ArgumentBag();
        var optionalParsingState = new OptionalParameterParsingState
        {
            Context = argumentBag
        };

        var result = optionalParsingState.Parse(args, index);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<ErrorParsingState>());
            Assert.That(((ErrorParsingState)argumentBag.ParsingState).ErrorMessage, Is.EqualTo("To many dash signs"));
        });
    }
    
    [Test]
    [TestCase(0, "--")]
    [TestCase(1, "--testing", "-")]
    [TestCase(1, "testing", "--")]
    public void NameIsNotProvided_MovingToParsingState(int index, params string[] args)
    {
        var argumentBag = new ArgumentBag();
        var optionalParsingState = new OptionalParameterParsingState
        {
            Context = argumentBag
        };
        
        var result = optionalParsingState.Parse(args, index);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<ErrorParsingState>());
            Assert.That(((ErrorParsingState)argumentBag.ParsingState).ErrorMessage, Is.EqualTo("No name provided"));
        });
    }
    
    [Test]
    [TestCase(0, "--1=")]
    [TestCase(0, "--1", "-1=")]
    [TestCase(1, "--1", "-1=")]
    [TestCase(1, "testing", "-1")]
    public void DigitFound_NegativeNumberParsingSate(int index, params string[] args)
    {
        var argumentBag = new ArgumentBag();
        var optionalParsingState = new OptionalParameterParsingState
        {
            Context = argumentBag
        };
        
        var result = optionalParsingState.Parse(args, index);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<NegativeNumberParsingState>());
        });
    }

    [Test]
    [TestCase(0, "--testing=")]
    [TestCase(1, "--testing", "--blob=")]
    [TestCase(1, "testing", "-c=")]
    public void ContainsEqualSign_MovingToParsingState(int index, params string[] args)
    {
        var argumentBag = new ArgumentBag();
        var optionalParsingState = new OptionalParameterParsingState
        {
            Context = argumentBag
        };
        
        var result = optionalParsingState.Parse(args, index);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<OptionalParameterValueParsingState>());
        });
    }

    [Test]
    [TestCase(0, "-testing")]
    [TestCase(0, "-testing", "-c")]
    [TestCase(1, "-c", "-testing")]
    public void InconsistentSingleDash_ErrorParsingState(int index, params string[] args)
    {
        var argumentBag = new ArgumentBag();
        var optionalParsingState = new OptionalParameterParsingState
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
    [TestCase(0, "--c")]
    [TestCase(0, "--c", "--testing")]
    [TestCase(1, "--testing", "--c")]
    public void InconsistentDoubleDash_ErrorParsingState(int index, params string[] args)
    {
        var argumentBag = new ArgumentBag();
        var optionalParsingState = new OptionalParameterParsingState
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
    [TestCase(0, "testing", "--testing")]
    [TestCase(1, "blob", "--testing", "--blob")]
    [TestCase(1, "c", "testing", "-c")]
    public void DefaultCase_BooleanOptionalParameter(int index, string parameterName, params string[] args)
    {
        var argumentBag = new ArgumentBag();
        var optionalParsingState = new OptionalParameterParsingState
        {
            Context = argumentBag
        };
        
        var result = optionalParsingState.Parse(args, index);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index + 1));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<InitialParsingState>());
            Assert.That(argumentBag[parameterName].hasValue, Is.True);
            Assert.That(argumentBag[parameterName].value, Is.EqualTo("true"));
        });
    }
}