import { isOddOrEven } from './isOddOrEven.js'
import { expect } from 'chai'


describe('isOddOrEven', () => {
    it('should return undefined if put non string - number input', () => {
        //Arrange
        const input = 123
        //Act
        const result = isOddOrEven(input)
        //Assert
        expect(result).to.be.undefined
    })

    it('should return undefined if put empty array input', () => {
        //Arrange
        //Act
        const nanResult = isOddOrEven(NaN);
        const undefinedResult = isOddOrEven(undefined);
        const objectResult = isOddOrEven({});
        const nullResult = isOddOrEven(null);
        //Assert
        expect(nanResult).to.be.undefined
        expect(undefinedResult).to.be.undefined
        expect(objectResult).to.be.undefined
        expect(nullResult).to.be.undefined
    })

    it('should return even if put string with even length input', () => {
        //Arrange
        const evenStringLength = 'is'
        //Act
        const result = isOddOrEven(evenStringLength)
        //Assert
        expect(result).to.equals('even')
    })

    it('should return odd if put string with odd length input', () => {
        //Arrange
        const oddStringLength = 'yes'
        //Act
        const result = isOddOrEven(oddStringLength)
        //Assert
        expect(result).to.equals('odd')
    })

    it('should return odd if put multiple string with odd length input', () => {
        //Arrange
        const input = 'Hello word! Be good'
        //Act
        const result = isOddOrEven(input)
        //Assert
        expect(result).to.equals('odd')
    })

})