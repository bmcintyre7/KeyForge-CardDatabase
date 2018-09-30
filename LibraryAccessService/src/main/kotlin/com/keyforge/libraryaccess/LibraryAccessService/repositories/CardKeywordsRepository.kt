package com.keyforge.libraryaccess.LibraryAccessService.repositories

import com.keyforge.libraryaccess.LibraryAccessService.data.CardExpansions
import com.keyforge.libraryaccess.LibraryAccessService.data.CardHouses
import com.keyforge.libraryaccess.LibraryAccessService.data.CardKeywords
import com.keyforge.libraryaccess.LibraryAccessService.data.Type
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.stereotype.Repository

@Repository
interface CardKeywordsRepository : JpaRepository<CardKeywords, Int> {
    fun findByCardId(id: Int) : List<CardKeywords>
    fun findByKeywordId(id: Int) : CardKeywords
}