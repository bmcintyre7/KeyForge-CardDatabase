package com.keyforge.libraryaccess.LibraryAccessService.data

import com.fasterxml.jackson.annotation.JsonIgnore
import org.hibernate.annotations.BatchSize
import javax.persistence.*

@Entity
@Table(name = "cardKeywords")
data class CardKeywords (
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    val id: Int? = null,
    @JsonIgnore
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "cardId")
    @BatchSize(size=50)
    val card: Card,
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "keywordId")
    @BatchSize(size=50)
    val keyword: Keyword,
    val value: String? = null
)