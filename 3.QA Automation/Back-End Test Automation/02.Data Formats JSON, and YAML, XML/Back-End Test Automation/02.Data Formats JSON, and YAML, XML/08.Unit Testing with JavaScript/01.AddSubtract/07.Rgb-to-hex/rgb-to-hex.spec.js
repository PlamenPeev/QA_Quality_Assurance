import { rgbToHexColor } from './rgb-to-hex.js'
import { expect } from 'chai'

describe('rgbToHexColor', () => {
    it('sould return undefined if red value is invalid', () => {
        //Arrange
        //Act
        const over255NumericRedValueResult = rgbToHexColor(999, 0, 0);
        const underZeroNumericRedValueResult = rgbToHexColor(-200, 0, 0);
        const nonNumericRedValueResult = rgbToHexColor('300', 0, 0);
        
        //Assert
        expect(over255NumericRedValueResult).to.be.undefined
        expect(underZeroNumericRedValueResult).to.be.undefined
        expect(nonNumericRedValueResult).to.be.undefined
    })

    it('sould return undefined if green value is invalid', () => {
        //Arrange
        //Act
        const over255NumericGreenValueResult = rgbToHexColor(0, 999, 0);
        const underZeroNumericGreenValueResult = rgbToHexColor(0, -100, 0);
        const nonNumericGreenValueResult = rgbToHexColor(0, '100', 0);
        
        //Assert
        expect(over255NumericGreenValueResult).to.be.undefined
        expect(underZeroNumericGreenValueResult).to.be.undefined
        expect(nonNumericGreenValueResult).to.be.undefined
    })

    it('sould return undefined if blue value is invalid', () => {
        //Arrange
        //Act
        const over255NumericBlueValueResult = rgbToHexColor(0, 0, 999);
        const underZeroNumericBlueValueResult = rgbToHexColor(0, 0, -150);
        const nonNumericBlueValueResult = rgbToHexColor(0, 0, '150');
        
        //Assert
        expect(over255NumericBlueValueResult).to.be.undefined
        expect(underZeroNumericBlueValueResult).to.be.undefined
        expect(nonNumericBlueValueResult).to.be.undefined
    })

    it('sould return a correct hex value if a currect rgb value is given', () => {
        //Arrange
        const red = 114;
        const green = 56;
        const blue = 87;
        //Act
        const result = rgbToHexColor(red,green,blue);
        //Assert
        expect(result).to.be.equals('#723857');
    })

    it('sould return undefined if red, green and blue value are invalid', () => {
        //Arrange
        const red = undefined;
        const green = '56';
        const blue = '87';
        //Act
        const result = rgbToHexColor(red,green,blue);
        //Assert
        expect(result).to.be.undefined;
    })

    it('sould return a correct hex value if a max rgb value is given', () => {
        //Arrange
        const red = 255;
        const green = 255;
        const blue = 255;
        //Act
        const result = rgbToHexColor(red,green,blue);
        //Assert
        expect(result).to.be.equals('#FFFFFF');
    })

    it('sould return a correct hex value if a min rgb value is given', () => {
        //Arrange
        const red = 0;
        const green = 0;
        const blue = 0;
        //Act
        const result = rgbToHexColor(red,green,blue);
        //Assert
        expect(result).to.be.equals('#000000');
    })
})