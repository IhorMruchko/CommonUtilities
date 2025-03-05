using CommonUtilities.Console.Entities;
using CommonUtilities.Console.Entities.ArgumentBagParsingStates;

namespace CommonUtilities.Testing.Console.ArgumentBagTesting;

public class NegativeNumberParsingStateTests
{
    [Test]
    [TestCase(0, "--2,4")]
    [TestCase(0, "--24")]
    [TestCase(0, "--17")]
    [TestCase(1, "test", "--17", "auto")]
    public void ToManyDashes_ErrorState(int index, params string[] arguments)
    {
        var argumentBag = new ArgumentBag();
        var negativeNumberState = new NegativeNumberParsingState
        {
            Context = argumentBag,
        };
        
        var result = negativeNumberState.Parse(arguments, index);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<ErrorParsingState>());
            Assert.That(
                ((ErrorParsingState)argumentBag.ParsingState).ErrorMessage, 
                Is.EqualTo("Number can not has more than one minus sign")
            );
        });
    }
    
    [Test]
    [TestCase(0, "-2,4")]
    [TestCase(0, "-24")]
    [TestCase(0, "-17")]
    [TestCase(1, "test", "-7   ", "auto")]
    public void DashesSet_MovedToNextArgument(int index, params string[] arguments)
    {
        var argumentBag = new ArgumentBag();
        var negativeNumberState = new NegativeNumberParsingState
        {
            Context = argumentBag,
        };
        
        var result = negativeNumberState.Parse(arguments, index);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(index + 1));
            Assert.That(argumentBag.ParsingState, Is.InstanceOf<InitialParsingState>());
            Assert.That(argumentBag[0].hasValue, Is.True);
            Assert.That(argumentBag[0].value, Is.EqualTo(arguments[index].Trim()));
        });
    }
}