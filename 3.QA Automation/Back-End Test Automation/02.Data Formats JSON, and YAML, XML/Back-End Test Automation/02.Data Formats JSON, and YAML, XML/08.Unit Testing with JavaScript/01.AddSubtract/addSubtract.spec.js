import { createCalculator } from './addSubtract.js'
import { expect } from 'chai'


describe('createCalculator', () => {
    it('should retur 0 if no put operation on calculator', () => {
        //Arrange
        const calculator = createCalculator()
        //Act
        const result = calculator.get()
        //Assert
        expect(result).to.equals(0)
    })

    it('should retur a negative number if only subtract operation executed on calculator', () => {
        //Arrange
        const calculator = createCalculator()
        //Act
        calculator.subtract(-2)
        calculator.subtract(-20)
        calculator.subtract(-10)
        const result = calculator.get()
        //Assert
        expect(result).to.equals(-32)
    })

    it('should retur a positive number if only add operation executed on calculator', () => {
        //Arrange
        const calculator = createCalculator()
        //Act
        calculator.add(2)
        calculator.add(20)
        calculator.add(10)
        const result = calculator.get()
        //Assert
        expect(result).to.equals(32)
    })

    it('should accept number as a string', () => {
        //Arrange
        const calculator = createCalculator()
        //Act
        calculator.add('2')
        calculator.add('20')
        calculator.add('10')
        const result = calculator.get()
        //Assert
        expect(result).to.equals(32)
    })

    it('should accept mix of operation', () => {
        //Arrange
        const calculator = createCalculator()
        //Act
        calculator.add('2')
        calculator.subtract('20')
        calculator.add('10')
        const result = calculator.get()
        //Assert
        expect(result).to.equals(-8)
    })
})