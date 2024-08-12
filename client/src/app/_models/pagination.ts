export interface Pagination {
    currentPage:number;
    itemsPerPage:number;
    totalItems:number;
    totalPages:number;

}

// values are optional because they have to be set here first
export class PaginatedResult<T> {
    items?: T;
    pagination?: Pagination;
}