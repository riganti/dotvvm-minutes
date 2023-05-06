export default context => new CalcService(context);

class CalcService {

    constructor(context) {
        this.context = context;
    }

    calculateTotal(operation, firstValue, value) {
        return operation == "+" ? firstValue + value
             : operation == "-" ? firstValue - value
             : operation == "*" ? firstValue * value
             : firstValue / value;
    }

}