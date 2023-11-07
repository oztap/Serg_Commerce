export interface Pagination<T> {
    pageIndex: number;
    pagesize: number;
    count: number;
    data: T;
  }