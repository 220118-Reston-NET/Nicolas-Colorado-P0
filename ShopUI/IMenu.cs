namespace ShopUI
{
    /* 
        Interface are one of the best ways to implement abstraction
        Every method is implicitly abstract, meaning you don't have
        to write anything. Every method is public.
    */
    public interface IMenu
    {
        /// <summary>
        /// Will dsiplay the menu and user choices in the terminal
        /// </summary>
        void Display(); //implicitly public


        /// <summary>
        /// Will record the user choice and change/route youy menu based on your choice
        /// 
        /// </summary>
        /// <returns>Return the menu that will change your screen</returns>
        string UserChoice();
    }

}