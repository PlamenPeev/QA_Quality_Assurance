import { mathEnforcer } from './mathEnforcer.js'
import { expect } from 'chai'

describe('mathEnforcer', () => {
    describe('addFive', () => {
        it('should return undefined if put not a number as input', () => {
            //Arrange
            const stringInput = 'hello'
            //Act
            const result = mathEnforcer.addFive(stringInput)
            //Assert
            expect(result).to.be.undefined
        })

        it('should return undefined if put undefined as input', () => {
            //Arrange
            const undefinedInput = undefined
            //Act
            const result = mathEnforcer.addFive(undefinedInput)
            //Assert
            expect(result).to.be.undefined
        })

        it('should return undefined if put number like string as input', () => {
            //Arrange
            const stringNumInput = '2'
            //Act
            const result = mathEnforcer.addFive(stringNumInput)
            //Assert
            expect(result).to.be.undefined
        })

        it('should return correct value if put positive number as input', () => {
            //Arrange
            const positiveNumInput = 3
            //Act
            const result = mathEnforcer.addFive(positiveNumInput)
            //Assert
            expect(result).to.be.equal(8)
        })

        it('should return correct value if put negative number as input', () => {
            //Arrange
            const negativeNumInput = -6
            //Act
            const result = mathEnforcer.addFive(negativeNumInput)
            //Assert
            expect(result).to.be.equal(-1)
        })

        it('should return correct value if put floating number as input and assert with closeTo', () => {
            //Arrange
            const floatingNumInput = 10.01
            //Act
            const result = mathEnforcer.addFive(floatingNumInput)
            //Assert
            expect(result).to.be.closeTo(15.01, 0.01)
        })

        it('should return correct value if put floating number as input and assert with equal', () => {
            //Arrange
            const floatingNumInput = 10.01
            //Act
            const result = mathEnforcer.addFive(floatingNumInput)
            //Assert
            expect(result).to.be.equal(15.01)
        })

        it('should return correct value if put floating number with a lot of digits as input and assert with closeTo', () => {
            //Arrange
            const floatingNumInput = 10.000000001
            //Act
            const result = mathEnforcer.addFive(floatingNumInput)
            //Assert
            expect(result).to.be.closeTo(15.01, 0.01)
        })

        it('should return correct value if put min floating number as input and assert with closeTo', () => {
            //Arrange
            const floatingNumInput = 0.01
            //Act
            const result = mathEnforcer.addFive(floatingNumInput)
            //Assert
            expect(result).to.be.closeTo(5.01, 0.01)
        })

    })

    describe('subtractTen', () => {
        it('should return undefined if put not a number as input', () => {
            //Arrange
            const stringInput = 'hello'
            //Act
            const result = mathEnforcer.subtractTen(stringInput)
            //Assert
            expect(result).to.be.undefined
        })

        it('should return undefined if put undefined as input', () => {
            //Arrange
            const undefinedInput = undefined
            //Act
            const result = mathEnforcer.subtractTen(undefinedInput)
            //Assert
            expect(result).to.be.undefined
        })

        it('should return undefined if put number like string as input', () => {
            //Arrange
            const stringNumInput = '2'
            //Act
            const result = mathEnforcer.subtractTen(stringNumInput)
            //Assert
            expect(result).to.be.undefined
        })

        it('should return correct value if put positive number as input', () => {
            //Arrange
            const positiveNumInput = 0
            //Act
            const result = mathEnforcer.subtractTen(positiveNumInput)
            //Assert
            expect(result).to.be.equal(-10)
        })

        it('should return correct value if put negative number as input', () => {
            //Arrange
            const negativeNumInput = -6
            //Act
            const result = mathEnforcer.subtractTen(negativeNumInput)
            //Assert
            expect(result).to.be.equal(-16)
        })

        it('should return correct value if put floating number as input and assert with closeTo', () => {
            //Arrange
            const floatingNumInput = 10.01
            //Act
            const result = mathEnforcer.subtractTen(floatingNumInput)
            //Assert
            expect(result).to.be.closeTo(0.009, 0.01)
        })

        it('should return correct value if put floating number as input and assert with equal', () => {
            //Arrange
            const floatingNumInput = 15.01
            //Act
            const result = mathEnforcer.subtractTen(floatingNumInput)
            //Assert
            expect(result).to.be.equal(5.01)
        })

        it('should return correct value if put floating number with a lot of digits as input and assert with closeTo', () => {
            //Arrange
            const floatingNumInput = 10.000000001
            //Act
            const result = mathEnforcer.subtractTen(floatingNumInput)
            //Assert
            expect(result).to.be.closeTo(0.01, 0.01)
        })

        it('should return correct value if put min floating number as input and assert with closeTo', () => {
            //Arrange
            const floatingNumInput = 0.01
            //Act
            const result = mathEnforcer.subtractTen(floatingNumInput)
            //Assert
            expect(result).to.be.closeTo(-9.99, 0.01)
        })
    })

    describe('sum', () => {
        it('should return undefined if put incorrect firstParam and incorrect secondParam as input', () => {
            //Arrange
            const firstParam = 'hello'
            const secondParam = 'hi'
            //Act
            const result = mathEnforcer.sum(firstParam,secondParam)
            //Assert
            expect(result).to.be.undefined
        })
        
        it('should return undefined if put correct firstParam and incorrect secondParam as input', () => {
            //Arrange
            const firstParam = 10
            const secondParam = 'hi'
            //Act
            const result = mathEnforcer.sum(firstParam,secondParam)
            //Assert
            expect(result).to.be.undefined
        })

        it('should return undefined if put incorrect firstParam and correct secondParam as input', () => {
            //Arrange
            const firstParam = 'hello'
            const secondParam = 10
            //Act
            const result = mathEnforcer.sum(firstParam,secondParam)
            //Assert
            expect(result).to.be.undefined
        })

        it('should return correct value if put correct firstParam and correct secondParam as input', () => {
            //Arrange
            const firstParam = 20
            const secondParam = 10
            //Act
            const result = mathEnforcer.sum(firstParam,secondParam)
            //Assert
            expect(result).to.be.equal(30)
        })

        it('should return correct value if put negative firstParam and negative secondParam as input', () => {
            //Arrange
            const firstParam = -20
            const secondParam = -10
            //Act
            const result = mathEnforcer.sum(firstParam,secondParam)
            //Assert
            expect(result).to.be.equal(-30)
        })

        it('should return correct value if put positive firstParam and negative secondParam as input', () => {
            //Arrange
            const firstParam = 20
            const secondParam = -10
            //Act
            const result = mathEnforcer.sum(firstParam,secondParam)
            //Assert
            expect(result).to.be.equal(10)
        })

        it('should return correct value if put negative firstParam and positive secondParam as input', () => {
            //Arrange
            const firstParam = -20
            const secondParam = 10
            //Act
            const result = mathEnforcer.sum(firstParam,secondParam)
            //Assert
            expect(result).to.be.equal(-10)
        })

        it('should return correct value if put correct firstParam and floating secondParam as input', () => {
            //Arrange
            const firstParam = 20
            const secondParam = 10.01
            //Act
            const result = mathEnforcer.sum(firstParam,secondParam)
            //Assert
            expect(result).to.be.equal(30.009999999999998)
        })

        it('should return correct value if put correct firstParam and floating secondParam as input', () => {
            //Arrange
            const firstParam = 20
            const secondParam = 10.01
            //Act
            const result = mathEnforcer.sum(firstParam,secondParam)
            //Assert
            expect(result).to.be.closeTo(30.01, 0.01)
        })
       
        it('should return correct value if put correct floating and floating secondParam as input', () => {
            //Arrange
            const firstParam = 20.01
            const secondParam = 10.01
            //Act
            const result = mathEnforcer.sum(firstParam,secondParam)
            //Assert
            expect(result).to.be.closeTo(30.02, 0.01)
        })


    })


})