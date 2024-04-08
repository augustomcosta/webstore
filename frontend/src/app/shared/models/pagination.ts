import { IProduct } from '../../core/models/IProduct';

export interface IPagination<T> {
  pageNumber: number;
  pageSize: number;
  count: number;
  data: T;
}
