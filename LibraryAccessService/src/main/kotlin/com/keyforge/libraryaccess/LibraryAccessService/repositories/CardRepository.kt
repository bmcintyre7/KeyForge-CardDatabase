package com.keyforge.libraryaccess.LibraryAccessService.repositories

import com.keyforge.libraryaccess.LibraryAccessService.data.Card
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.stereotype.Repository
import org.springframework.data.jpa.repository.JpaSpecificationExecutor
import org.springframework.data.jpa.repository.Query
import org.springframework.data.repository.query.Param

@Repository
interface CardRepository : JpaRepository<Card, Int>, JpaSpecificationExecutor<Card> {
    @Query(value=""" FROM Card WHERE name LIKE %:searchVal% OR LOWER(name) = LOWER(:searchVal) """)
    fun searchByName(@Param("searchVal") searchVal: String): MutableList<Card>

    @Query(value=""" FROM Card WHERE name LIKE :searchVal%""")
    fun startsWithByName(@Param("searchVal") searchVal: String): MutableList<Card>
}