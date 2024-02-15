import {isSymmetric} from './checkForSymmetry.js'
import { expect } from 'chai'

describe('isSymmetric', () => {
    it('should return true if an empty array is given', () => {
        //Arrange
        const inputArray = [];

        //Act
        const result = isSymmetric(inputArray)

        //Assert
        expect(result).to.be.true
    })

    it('should return false if an non-array value is given', () => {
        //Arrange
      
        //Act
        const nanResult = isSymmetric(NaN);
        const undefinedResult = isSymmetric(undefined);
        const objectResult = isSymmetric({});
        const nullResult = isSymmetric(null);
        const stringResult = isSymmetric('string');
        const numbersResult = isSymmetric(123);

        //Assert
        expect(nanResult).to.be.false
        expect(undefinedResult).to.be.false
        expect(objectResult).to.be.false
        expect(nullResult).to.be.false
        expect(stringResult).to.be.false
        expect(numbersResult).to.be.false
    })

    it('should return false if an non symmetric array value is given', () => {
        //Arrange
        const inputArray = [1,2,3,4];

        //Act
        const result = isSymmetric(inputArray)

        //Assert
        expect(result).to.be.false
    })

    it('should return true if an symmetric array value is given', () => {
        //Arrange
        const inputArray = [1,2,3,2,1];

        //Act
        const result = isSymmetric(inputArray)

        //Assert
        expect(result).to.be.true
    })

    it('should return false if an non symmetric array value is given', () => {
        //Arrange
        const inputArray = [1,2,3,'2','1'];

        //Act
        const result = isSymmetric(inputArray)

        //Assert
        expect(result).to.be.false
    })
})