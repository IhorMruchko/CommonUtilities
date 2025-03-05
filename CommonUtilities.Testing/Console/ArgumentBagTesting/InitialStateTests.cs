using CommonUtilities.Console.Entities;
using CommonUtilities.Console.Entities.ArgumentBagParsingStates;

namespace CommonUtilities.Testing.Console.ArgumentBagTesting;

public class InitialStateTests
{
    [Test]
    [TestCase]
    [TestCase("test")]
    [TestCase("test", "test")]
    public void LastArgument_ParsingCompleted(params string[] args)
    {
        var argumentBag = new ArgumentBag(args);
        var initialState = new InitialParsingState
        {
            Context = argumentBag
        };

        var result = initialState.Parse(args, args.Length);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(args.Length));
            Assert.That(argumentBag.ParsingState.GetType(), Is.EqualTo(typeof(CompletedParsingState)));
        });
    }

    [Test]
    [TestCase(0, "--test")]
    [TestCase(0, "--test", "blob")]
    [TestCase(1, "test", "--blob")]
    [TestCase(1, "test", "--blob", "test")]
    public void OptionalArgument_TransitionToRightState(int index, params string[] args)
    {
        var argumentBag = new ArgumentBag(args);
        var initialState = new InitialParsingState
        {
            Context = argumentBag
        };
        
        var result = initialState.Parse(args, index);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index));
            Assert.That(argumentBag.ParsingState.GetType(), Is.EqualTo(typeof(OptionalParameterParsingState)));
        });

    }
    
    [Test]
    [TestCase(0)]
    [TestCase(1, "test")]
    [TestCase(10, "test", "test")]
    public void IndexMoreThatLength_ParsingCompleted(int index, params string[] args)
    {
        var argumentBag = new ArgumentBag(args);
        var initialState = new InitialParsingState
        {
            Context = argumentBag
        };

        var result = initialState.Parse(args, index);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index));
            Assert.That(argumentBag.ParsingState.GetType(), Is.EqualTo(typeof(CompletedParsingState)));
        });
    }
    
    
    [Test]
    [TestCase(0, "test")]
    [TestCase(0, "test", "blob")]
    [TestCase(1, "test", "blob")]
    [TestCase(1, "--test", "blob")]
    public void PositionalParameterFound_ParameterAdded(int index, params string[] args)
    {
        var argumentBag = new ArgumentBag();
        var initialState = new InitialParsingState
        {
            Context = argumentBag
        };

        var result = initialState.Parse(args, index);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index + 1));
            Assert.That(argumentBag.ParsingState.GetType(), Is.EqualTo(typeof(InitialParsingState)));
            Assert.That(argumentBag[0].hasValue, Is.True);
            Assert.That(argumentBag[0].value, Is.EqualTo(args[index]));
            Assert.That(argumentBag[1].hasValue, Is.False);
            Assert.That(argumentBag[1].value, Is.Null);
        });
    }
}