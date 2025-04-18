# Equality Semantics in .NET

> 🗃️ *A chapter from The .NET Codex*  
> *Because real understanding doesn’t come with a job title.*

> *Not all equals are created equal.*

Equality in .NET is deceptively tricky. On the surface, it seems simple: `==` means equal, `Equals` does... something similar, and `ReferenceEquals` is rarely used, right?

But beneath that surface lies a maze of overloads, hidden assumptions, and subtle behaviors that can trip up even experienced developers. Value types vs. reference types, operator overloads, boxing, and custom equality implementations — it’s all too easy to get wrong, and even easier to *never realize you did*.

This article is a guided deep dive into the three pillars of equality in .NET:
- `==` operator
- `Equals()` method
- `ReferenceEquals()` method

We’ll cover when to use which, how they behave differently with value vs. reference types, how to implement custom equality correctly, and where common misunderstandings creep in.

Whether you're writing your first struct or reviewing code that just isn't behaving the way you expected, this article is here to clarify, demystify, and equip you to *really* understand equality in .NET.

[Read the article →](./article.md)

