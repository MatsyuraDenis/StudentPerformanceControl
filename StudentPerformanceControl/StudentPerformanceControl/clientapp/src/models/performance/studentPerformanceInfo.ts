import {SubjectStudentPerformanceDto} from "./subjectStudentPerformanceDto";

export class StudentPerformanceInfo {
    studentId: number;
    name: string;
    secondName: string;
    subjects: SubjectStudentPerformanceDto[]
}