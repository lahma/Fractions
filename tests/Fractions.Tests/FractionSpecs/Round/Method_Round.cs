﻿using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;
using Tests.Fractions;

namespace Fractions.Tests.FractionSpecs.Round;

[TestFixture]
public class When_rounding_a_decimal_fraction : Spec {
    private static readonly Fraction[] DecimalFractions = [
        0, 1, -1, 10, -10,
        0.5m, -0.5m, 0.55m, -0.55m, 1.5m, -1.5m, 1.55m, -1.55m,
        0.1m, -0.1m, 0.15m, -0.15m, 1.2m, -1.2m, 1.25m, -1.25m,
        1.5545665434654m, -1.5545665434654m,
        15.545665434654m, -15.545665434654m,
        155.45665434654m, -155.45665434654m,
        1554.5665434654m, -1554.5665434654m,
        15545.665434654m, -15545.665434654m,
        155456.65434654m, -155456.65434654m,
        new Fraction(1, 3), new Fraction(-1, 3),
        new Fraction(2, 3), new Fraction(-2, 3),
        new Fraction(1999999999999999991, 3), new Fraction(-1999999999999999991, 3),
        new Fraction(2000000000000000001, 3), new Fraction(-2000000000000000001, 3)
    ];

    private static readonly int[] DecimalPlaces = Enumerable.Range(0, 6).ToArray();

    private static readonly MidpointRounding[] RoundingModes = [
        MidpointRounding.ToEven,
        MidpointRounding.AwayFromZero,
#if NET
        MidpointRounding.ToZero,
        MidpointRounding.ToNegativeInfinity,
        MidpointRounding.ToPositiveInfinity
#endif
    ];

    private static IEnumerable RoundToBigIntegerTestCases =>
        from decimalFraction in DecimalFractions
        from midpointRounding in RoundingModes
        select new TestCaseData(decimalFraction, midpointRounding)
            .Returns((BigInteger)decimal.Round((decimal)decimalFraction, midpointRounding));


    [Test]
    [TestCaseSource(nameof(RoundToBigIntegerTestCases))]
    public BigInteger The_integral_result_should_be_correct_for_all_decimal_values(Fraction fraction, MidpointRounding roundingMode) {
        return Fraction.RoundToBigInteger(fraction, roundingMode);
    }

    private static IEnumerable RoundToFractionTestCases =>
        from decimalFraction in DecimalFractions
        from decimalPlaces in DecimalPlaces
        from midpointRounding in RoundingModes
        select new TestCaseData(decimalFraction, decimalPlaces, midpointRounding)
            .Returns(new Fraction(decimal.Round((decimal)decimalFraction, decimalPlaces, midpointRounding)));

    [Test]
    [TestCaseSource(nameof(RoundToFractionTestCases))]
    public Fraction The_fractional_result_should_be_correct_for_all_decimal_values(Fraction fraction, int decimalPlaces, MidpointRounding roundingMode) {
        return Fraction.Round(fraction, decimalPlaces, roundingMode);
    }

    private static IEnumerable RoundRepeatingFractionTestCases {
        get {
            foreach (var roundingMode in new[] { MidpointRounding.ToEven, MidpointRounding.AwayFromZero }) {
                // one third: (1/3) rounded to 2 decimals should always be 0.33 (33/100)
                yield return new TestCaseData(new Fraction(1, 3), 2, roundingMode)
                    .Returns(new Fraction(33, 100)); // 0.33
                // minus one third: (-1/3) rounded to 2 decimals should always be -0.33 (-33/100)
                yield return new TestCaseData(new Fraction(-1, 3), 2, roundingMode)
                    .Returns(new Fraction(-33, 100)); // -0.33
            }

#if NET
            foreach (var roundingMode in new[] { MidpointRounding.ToNegativeInfinity, MidpointRounding.ToZero }) {
                // one third: (1/3) rounded to 2 decimals should always be 0.33 (33/100)
                yield return new TestCaseData(new Fraction(1, 3), 2, roundingMode)
                    .Returns(new Fraction(33, 100)); // 0.33
            }

            // one third: (1/3) rounded to 2 decimals should always be 0.34 (34/100) when rounding up
            yield return new TestCaseData(new Fraction(1, 3), 2, MidpointRounding.ToPositiveInfinity)
                .Returns(new Fraction(34, 100)); // 0.34

            foreach (var roundingMode in new[] { MidpointRounding.ToZero, MidpointRounding.ToPositiveInfinity }) {
                // minus one third: (-1/3) rounded to 2 decimals should always be -0.33 (-33/100)
                yield return new TestCaseData(new Fraction(-1, 3), 2, roundingMode)
                    .Returns(new Fraction(-33, 100)); // -0.33
            }

            // minus one third: (-1/3) rounded to 2 decimals should always be -0.34 (-34/100) when rounding down
            yield return new TestCaseData(new Fraction(-1, 3), 2, MidpointRounding.ToNegativeInfinity)
                .Returns(new Fraction(-34, 100)); // -0.34
#endif
            foreach (var roundingMode in new[] { MidpointRounding.ToEven, MidpointRounding.AwayFromZero }) {
                // two thirds: (2/3) rounded to 2 decimals should always be 0.67 (67/100)
                yield return new TestCaseData(new Fraction(2, 3), 2, roundingMode)
                    .Returns(new Fraction(67, 100)); // 0.67
                // minus one third: (-2/3) rounded to 2 decimals should always be -0.67 (-67/100)
                yield return new TestCaseData(new Fraction(-2, 3), 2, roundingMode)
                    .Returns(new Fraction(-67, 100)); // -0.67
            }
#if NET
            // two thirds: (2/3) rounded to 2 decimals should always be 0.67 (67/100)
            yield return new TestCaseData(new Fraction(2, 3), 2, MidpointRounding.ToPositiveInfinity)
                .Returns(new Fraction(67, 100)); // 0.67

            foreach (var roundingMode in new[] { MidpointRounding.ToZero, MidpointRounding.ToNegativeInfinity }) {
                // two thirds: (2/3) rounded to 2 decimals should always be 0.66 (66/100) when rounded down
                yield return new TestCaseData(new Fraction(2, 3), 2, roundingMode)
                    .Returns(new Fraction(66, 100)); // 0.66
            }

            foreach (var roundingMode in new[] { MidpointRounding.ToZero, MidpointRounding.ToPositiveInfinity }) {
                // minus two thirds: (-2/3) rounded to 2 decimals should always be -0.66 (-66/100)
                yield return new TestCaseData(new Fraction(-2, 3), 2, roundingMode)
                    .Returns(new Fraction(-66, 100)); // 0.66
            }

            // minus two thirds: (-2/3) rounded to 2 decimals should always be -0.67 (-67/100) when rounded down
            yield return new TestCaseData(new Fraction(-2, 3), 2, MidpointRounding.ToNegativeInfinity)
                .Returns(new Fraction(-67, 100)); // 0.67
#endif
            
            foreach (var roundingMode in new[] { MidpointRounding.ToEven, MidpointRounding.AwayFromZero }) {
                // four thirds: (4/3) rounded to 2 decimals should always be 1.33 (133/100)
                yield return new TestCaseData(new Fraction(4, 3), 2, roundingMode)
                    .Returns(new Fraction(133, 100)); // 1.33
                // minus four thirds (-4/3) rounded to 2 decimals should always be -1.33 (-133/100)
                yield return new TestCaseData(new Fraction(-4, 3), 2, roundingMode)
                    .Returns(new Fraction(-133, 100)); // -1.33
            }

#if NET
            foreach (var roundingMode in new[] { MidpointRounding.ToNegativeInfinity, MidpointRounding.ToZero }) {
                // four thirds: (4/3) rounded to 2 decimals should always be 1.33 (133/100)
                yield return new TestCaseData(new Fraction(4, 3), 2, roundingMode)
                    .Returns(new Fraction(133, 100)); // 1.33
            }

            // four thirds: (4/3) rounded to 2 decimals should always be 1.34 (134/100) when rounding up
            yield return new TestCaseData(new Fraction(4, 3), 2, MidpointRounding.ToPositiveInfinity)
                .Returns(new Fraction(134, 100)); // 1.34
            
            foreach (var roundingMode in new[] { MidpointRounding.ToZero, MidpointRounding.ToPositiveInfinity }) {
                // minus four thirds: (-4/3) rounded to 2 decimals should always be -1.33 (-133/100)
                yield return new TestCaseData(new Fraction(-4, 3), 2, roundingMode)
                    .Returns(new Fraction(-133, 100)); // -1.33
            }
            
            // minus four thirds: (-4/3) rounded to 2 decimals should always be -1.34 (-134/100) when rounding down
            yield return new TestCaseData(new Fraction(-4, 3), 2, MidpointRounding.ToNegativeInfinity)
                .Returns(new Fraction(-134, 100)); // -1.34
#endif
        }
    }


    [Test]
    [TestCaseSource(nameof(RoundRepeatingFractionTestCases))]
    public Fraction The_fractional_result_should_be_correct_for_all_repeating_fractions(Fraction fraction, int decimalPlaces, MidpointRounding roundingMode) {
        return Fraction.Round(fraction, decimalPlaces, roundingMode);
    }

    [Test]
    public void The_fractional_result_should_be_correct_for_very_large_values() {
        var a = new BigInteger(123456789987654321);
        var b = new BigInteger(123456789987654321) * BigInteger.Pow(10, 18);
        var largeValue = a + b; // should represent the value 123456789987654321123456789987654321
        var middle = new Fraction(largeValue, BigInteger.Pow(10, 18)); // place the decimal point in the middle
        var valueToTest = middle / BigInteger.Pow(10, 17);

        var roundedValue = Fraction.Round(valueToTest, 36);

        roundedValue.Should().Be(valueToTest);
    }
}