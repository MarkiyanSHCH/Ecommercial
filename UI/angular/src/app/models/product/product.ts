import { Property } from "./property";

export interface Product {
    id: number;
    name: string;
    description: string;
    price: number;
    categoryId: number;
    photoFileName: string;
    properties: Property[];
}
