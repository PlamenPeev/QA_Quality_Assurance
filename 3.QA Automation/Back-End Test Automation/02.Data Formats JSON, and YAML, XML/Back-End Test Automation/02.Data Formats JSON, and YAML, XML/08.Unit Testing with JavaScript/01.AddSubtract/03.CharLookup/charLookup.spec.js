import { lookupChar } from './charLookup.js'
import { expect } from 'chai'

describe('lookupChar', () => {
    it('should return undefined if a given non string and non number input', () => {
        //Arrange
        const nonString = 123;
        const nonNumber = '123';
        //Act
        const undefinedResult = lookupChar(nonString,nonNumber);
        //Assert
        expect(undefinedResult).to.be.undefined
    })

    it('should return undefined if a given non string and correct number input', () => {
        //Arrange
        const nonString = 123;
        const nonNumber = 1;
        //Act
        const undefinedResult = lookupChar(nonString,nonNumber);
        //Assert
        expect(undefinedResult).to.be.undefined
    })

    it('should return undefined if a given correct string and non number input', () => {
        //Arrange
        const nonString = 'yes';
        const nonNumber = '1';
        //Act
        const undefinedResult = lookupChar(nonString,nonNumber);
        //Assert
        expect(undefinedResult).to.be.undefined
    })


    it('should return undefined if a given correct string and floating number input', () => {
        //Arrange
        const nonString = 'yes';
        const nonNumber = 12.3;
        //Act
        const undefinedResult = lookupChar(nonString,nonNumber);
        //Assert
        expect(undefinedResult).to.be.undefined
    })

    it('should return Incorrect index if a given correct string and bigger number input', () => {
        //Arrange
        const nonString = 'yes';
        const nonNumber = 123;
        //Act
        const undefinedResult = lookupChar(nonString,nonNumber);
        //Assert
        expect(undefinedResult).to.be.equals('Incorrect index')
    })

    it('should return Incorrect index if a given correct string and equal to the string length number input', () => {
        //Arrange
        const nonString = '123';
        const nonNumber = 3;
        //Act
        const undefinedResult = lookupChar(nonString,nonNumber);
        //Assert
        expect(undefinedResult).to.be.equals('Incorrect index')
    })

    it('should return Incorrect index if a given correct string and negative number input', () => {
        //Arrange
        const nonString = '123';
        const nonNumber = -3;
        //Act
        const undefinedResult = lookupChar(nonString,nonNumber);
        //Assert
        expect(undefinedResult).to.be.equals('Incorrect index')
    })


    it('should return char at the specified index if a given correct string and number input', () => {
          //Arrange
          const nonString = 'Hello';
          const nonNumber = 4;
          //Act
          const undefinedResult = lookupChar(nonString,nonNumber);
          //Assert
          expect(undefinedResult).to.be.equals('o')
    })
})