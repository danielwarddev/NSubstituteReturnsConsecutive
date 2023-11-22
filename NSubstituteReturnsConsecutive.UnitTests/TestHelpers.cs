using NSubstitute;
using NSubstitute.Core;

namespace NSubstituteReturnsConsecutive.UnitTests;

public static class TestHelpers
{
    public static ConfiguredCall ReturnsConsecutive<T>(
        this T value, int timesToReturn, T returnValue, T returnValueAfter)
    {
        if (timesToReturn < 0)
        {
            throw new ArgumentException("Value must be non-negative", nameof(timesToReturn));
        }
        var returnValues = Enumerable
            .Repeat(returnValue, timesToReturn - 1)
            .Append(returnValueAfter)
            .ToArray();
        return value.Returns(returnValue, returnValues);
    }
}