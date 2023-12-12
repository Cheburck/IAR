using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая
{
    public abstract class AbstractFactory
    {
        public abstract MakeProducts CreateProduct();
        public abstract Form CreateWindow(MakeProducts product, Form1 form1);
    }
    
    //ConcreteFactory1  - Создает форму, на которой рисуется соответсвующий граф и используются необходимые для его построения функции
    class DagConcreteFactory: AbstractFactory
    {
        //SingleTon
        private static readonly DagConcreteFactory instance = new DagConcreteFactory();
        public static DagConcreteFactory Instance { get { return instance; } }
        protected DagConcreteFactory() { }
        //
        public override MakeProducts CreateProduct()
        {
            return new DagProduct();
        }
        public override Form CreateWindow(MakeProducts product, Form1 form1)
        {
            return new Ориентированный((DagProduct)product, form1);
        }
    }

    //ConcreteFactory2  - Создает форму, на которой рисуется соответсвующий граф и используются необходимые для его построения функции
    class UnDagConcreteFactory : AbstractFactory
    {
        //SingleTon
        private static readonly UnDagConcreteFactory instance = new UnDagConcreteFactory();
        public static UnDagConcreteFactory Instance { get { return instance; } }
        protected UnDagConcreteFactory() { }
        //
        public override MakeProducts CreateProduct()
        {
            return new UnDagProduct();
        }
        public override Form CreateWindow(MakeProducts product, Form1 form1)
        {
            return new НеОриент((UnDagProduct)product, form1);
        }
    }

    /////////
    ///
    public class Client
    {
        private Form alg;
        private MakeProducts product;
        public Client(AbstractFactory factory, Form1 form1)
        {
            product = factory.CreateProduct();
            alg = factory.CreateWindow(product, form1);
        }
        public void Run()
        {
            alg.Show();
        }
    }
}
