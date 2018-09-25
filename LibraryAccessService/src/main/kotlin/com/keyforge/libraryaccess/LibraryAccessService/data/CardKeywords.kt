package com.keyforge.libraryaccess.LibraryAccessService.data

import javax.persistence.*

@Entity
@Table(name = "cardKeywords")
data class CardKeywords (
    @Id
    val id: Int = 0,
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "cardId")
    val card: Card,
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "keywordId")
    val keyword: Keyword
)