let data: number | string;

data='42';

data=10;

interface Icar{
    color:string;
    model:string;
    topspeed?:number;
}
const car1:Icar={
    color:'blue',
    model:'bmw'
};

const car2:Icar={
    color:'red',
    model:'mercedes',
    topspeed:100
};

const multiply=(x:number,y:number): string=>{return (x*y).toString();};