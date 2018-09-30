package com.keyforge.libraryaccess.LibraryAccessService.repositories

import com.keyforge.libraryaccess.LibraryAccessService.data.CardExpansions
import com.keyforge.libraryaccess.LibraryAccessService.data.Type
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.stereotype.Repository

@Repository
interface CardExpansionsRepository : JpaRepository<CardExpansions, Int> {
    fun findByCardId(id: Int) : List<CardExpansions>
    fun findByExpansionId(id: Int) : List<CardExpansions>
    fun findByNumber(number: String): List<CardExpansions>
}