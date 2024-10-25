
const userName = import.meta.env.VITE_USER;
const password = import.meta.env.VITE_PASSWORD;

export const getBasicAuthToken = (): string => {
    return btoa(`${userName}:${password}`);
};
