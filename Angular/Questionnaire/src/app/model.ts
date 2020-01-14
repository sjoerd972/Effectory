export class Survey {
  constructor(public id: number, public title: string) { }
}

export class Question {
  constructor(public id: number, public text: string, public answerOptions: AnswerOption[]) {}
}

export class AnswerOption {
  constructor(public id: number, public text: string) {}
}

export class Answer {
  constructor(public id: number, public text: string, public answerOption: AnswerOption) {}
}
