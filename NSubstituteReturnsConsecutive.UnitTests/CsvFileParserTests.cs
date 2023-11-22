using FluentAssertions;
using NSubstitute;

namespace NSubstituteReturnsConsecutive.UnitTests;

public class CsvFileParserTests
{
    private readonly CsvFileParser _parser;
    private readonly ICsvLineParser _lineParser = Substitute.For<ICsvLineParser>();

    private static readonly string[] _specialCommaValues =
    {
        "some, values",
        "that, all",
        "have, commas",
        "in, them"
    };

    public CsvFileParserTests()
    {
        _parser = new CsvFileParser(_lineParser, _specialCommaValues);
    }

    [Fact]
    public void Parses_Special_And_Normal_Values()
    {
        var expectedValues = new Dictionary<string, int>
        {
            { _specialCommaValues[0], 2 },
            { _specialCommaValues[1], 2 },
            { _specialCommaValues[2], 2 },
            { _specialCommaValues[3], 0 },
            { "new", 1 }, { "values", 1 }, { "here", 1 },  
        };
        
        _lineParser
            .ExtractValueFromLine(Arg.Any<string>())
            .ReturnsConsecutive(3,
                new LineData(2, "doesn't matter"),
                new LineData(0, "new,values,here"));

        var actualValues = _parser.ParseLineValues("doesn't matter");

        actualValues.Should().BeEquivalentTo(expectedValues);
    }
}