import { sum } from './sum.js'
import { expect } from 'chai';

describe('sum function', () => {
    it('should return 0 if an empty array is given', () => {
        //Arrange
        const inputArray = [];

        //Act
        const result = sum(inputArray)

        //Assert
        expect(result).to.equals(0)
    })

    it('should return same number if an single number is given', () => {
        //Arrange
        const inputArray = [5];

        //Act
        const result = sum(inputArray)

        //Assert
        expect(result).to.equals(5)
    })

    it('should return sum of numbers if an multy number array is given', () => {
        //Arrange
        const inputArray = [1,2,3,4,5];

        //Act
        const result = sum(inputArray)

        //Assert
        expect(result).to.equals(15)
    })

    
})