import {HomeworkStudentResult} from "./homeworkStudentResult";

export class SubjectStudentPerformanceDto {
    subjectId: number;
    subjectTitle: string;
    module1Points: number;
    module1MaxPoints: number;
    module2Points: number;
    module2MaxPoints: number;
    examPoints: number;
    examMaxPoints: number;
    homeworks: HomeworkStudentResult[];
}