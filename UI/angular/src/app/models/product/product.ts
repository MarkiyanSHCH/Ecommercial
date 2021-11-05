import { Characteristic } from "./characteristic";

export interface Product {
    id: number;
    name: string;
    description: string;
    price: number;
    categoryId: number;
    photoFileName: string;
    characteristics: Characteristic[];
}
