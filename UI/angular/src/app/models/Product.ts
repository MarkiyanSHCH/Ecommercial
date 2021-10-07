import { Characteristic } from "./Characteristic";

export interface Product{
    id: number;
    name: string;
    description: string;
    price: number;
    categoryId: number;
    photoFileName: string;
    characteristics: Characteristic[]
}
