import { ToastContainer } from "react-toastify";
import { TextProcessorPage } from "./pages/TextProcessorPage";
import 'react-toastify/dist/ReactToastify.css';

function App() {

    return (
        <div>
            <div className="bg-gradient-to-r from-cyan-500 to-blue-500  min-h-screen">
                <div className="px-6 py-24 sm:px-6 sm:py-32 lg:px-8">
                    <div className="mx-auto max-w-2xl text-center">
                        <h2 className="text-balance text-4xl font-semibold tracking-tight text-white sm:text-5xl">
                            EPAM Systems - Code Test - Omar Soto
                        </h2>
                        <p className="mx-auto mt-6 max-w-xl text-pretty text-lg/8 text-white">
                            Simulate a hard processing task for input data items provided by the user. 
                        </p>
                        <div className="mt-10 flex items-center justify-center gap-x-6">
                            <ToastContainer
                                position="top-right"
                                autoClose={3000}
                                hideProgressBar={false}
                                newestOnTop={false}
                                closeOnClick
                                theme="light"
                            />
                            <TextProcessorPage />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default App;