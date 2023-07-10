import {configureStore} from '@reduxjs/toolkit'
import {boxesApi} from './services/boxes'

export const store = configureStore({
    reducer: {
        [boxesApi.reducerPath]: boxesApi.reducer,
    },
    // adding the api middleware enables caching, invalidation, polling and other features of `rtk-query`
    middleware: (getDefaultMiddleware) =>
        getDefaultMiddleware().concat(boxesApi.middleware),
})