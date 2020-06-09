import {endpoints} from "./endpoints";

export const api = {
  getStudentPerformance: function (studentId) {
    let res;
    let error;
    // eslint-disable-next-line no-undef
    axios.get(endpoints.getStudentPerformance, {
      params: studentId
    })
      .then(response => res = response.data)
      .catch(error => {
        error.isError = true;
        error.Error = error;
        error.message = "student not found";
        return error;
      });

    return res == undefined ? error : res;
  },
  getGroupsInfo: function () {
    let groups;
    // eslint-disable-next-line no-undef
    axios.get(endpoints.getGroupsInfo)
      .then(response => groups = response.data)
      .catch(error => {
        error.isError = true;
        error.Error = error;
        error.message = "Internal error";
        return error;
      });

    return groups;
  }
};