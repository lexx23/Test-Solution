import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

export const boxesApi = createApi({
    reducerPath: 'boxApi',
    baseQuery: fetchBaseQuery({ baseUrl: process.env.REACT_APP_BASE_URL }),
    tagTypes: ['Box'],
    endpoints: (builder) => ({
        getBoxes: builder.query({
            query: () => '/boxes',
            providesTags: (result, error, arg) =>
                result
                    ? [...result.map(({ id }) => ({ type: 'Box', id })), 'Box']
                    : ['Box'],
        }),
        addBox: builder.mutation({
            query: (body) => ({
                url: `/boxes`,
                method: 'POST',
                body,
            }),
            invalidatesTags: ['Box'],
        }),
        deleteBox: builder.mutation({
            query: (id) => ({
                url: `/boxes/${id}`,
                method: 'DELETE',
            }),
            invalidatesTags: ['Box'],
        }),
    }),
})

// Export hooks for usage in functional components
export const { useGetBoxesQuery, useAddBoxMutation, useDeleteBoxMutation } = boxesApi