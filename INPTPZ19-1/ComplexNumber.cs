namespace INPTPZ1
{
    class ComplexNumber
    {
        public double Re { get; set; }
        public double Im { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Re = 0,
            Im = 0
        };

        public ComplexNumber Multiply(ComplexNumber complexNumber)
        {
            return new ComplexNumber()
            {
                Re = this.Re * complexNumber.Re - this.Im * complexNumber.Im,
                Im = this.Re * complexNumber.Im + this.Im * complexNumber.Re
            };
        }

        public ComplexNumber Add(ComplexNumber complexNumber)
        {
            return new ComplexNumber()
            {
                Re = this.Re + complexNumber.Re,
                Im = this.Im + complexNumber.Im
            };
        }
        public ComplexNumber Subtract(ComplexNumber complexNumber)
        {
            return new ComplexNumber()
            {
                Re = this.Re - complexNumber.Re,
                Im = this.Im - complexNumber.Im
            };
        }

        public override string ToString()
        {
            return $"({Re} + {Im}i)";
        }

        internal ComplexNumber Divide(ComplexNumber complexNumber)
        {
            ComplexNumber dividedComplexNumber = this.Multiply(
                new ComplexNumber() {
                    Re = complexNumber.Re,
                    Im = -complexNumber.Im });
            double divisor = complexNumber.Re * complexNumber.Re + complexNumber.Im * complexNumber.Im;

            return new ComplexNumber()
            {
                Re = dividedComplexNumber.Re / divisor,
                Im = dividedComplexNumber.Im / divisor
            };
        }
    }
}
