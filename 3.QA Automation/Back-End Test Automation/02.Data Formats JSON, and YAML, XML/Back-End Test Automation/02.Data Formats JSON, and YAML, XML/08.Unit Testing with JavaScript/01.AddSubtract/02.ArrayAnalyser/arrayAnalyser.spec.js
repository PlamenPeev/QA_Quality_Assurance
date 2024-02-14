import {analyzeArray} from './arrayAnalyser.js'
import { expect } from 'chai'

describe('analyzeArray', () => {
    it('should return undefined when the input is a non-array input', () => {
        //Arrange
        const stringInput = 'hello'
        //Act
        const result = analyzeArray(stringInput)
        //Asser
        expect(result).to.be.undefined
    })

    it('should return undefined when the input is a non-array number input', () => {
        //Arrange
        const numberInput = 123
        //Act
        const result = analyzeArray(numberInput)
        //Asser
        expect(result).to.be.undefined
    })

    it('should return undefined when the input is an empty array', () => {
        //Arrange
        const emptyInput = []
        //Act
        const result = analyzeArray(emptyInput)
        //Asser
        expect(result).to.be.undefined
    })

    it('should return correct when the input is an correct array of numbers', () => {
        //Arrange
        const input = [1, 2, 3, -1, -10]
        //Act
        const result = analyzeArray(input)
        //Asser
        expect(result).to.deep.equal({min: -10, max: 3, length: 5})
    })

    it('should return correct when the input is an array with equal elements', () => {
        //Arrange
        const input = [1, 1, 1, 1, 1]
        //Act
        const result = analyzeArray(input)
        //Asser
        expect(result).to.deep.equal({min: 1, max: 1, length: 5})
    })

    it('should return correct when the input is a single element array', () => {
        //Arrange
        const input = [15]
        //Act
        const result = analyzeArray(input)
        //Asser
        expect(result).to.deep.equal({min: 15, max: 15, length: 1})
    })

    it('should return undefined for an array with mixed elements', () => {
        //Arrange
        const input = [15, '2', 3, '1']
        //Act
        const result = analyzeArray(input)
        //Asser
        expect(result).to.be.undefined
    })

    it('should return correct for an array with mixed integer and floating numbers', () => {
        //Arrange
        const input = [15, 2.5, 3, 1.6]
        //Act
        const result = analyzeArray(input)
        //Asser
        expect(result).to.deep.equal({min: 1.6, max: 15, length: 4})
    })
})